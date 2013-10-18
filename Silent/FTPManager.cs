using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logger
{
	class FTPManager
	{
		private readonly static string nl = Environment.NewLine;
		private readonly static char dsp = Path.DirectorySeparatorChar;

		private string host;
		private string username;
		private string password;

		private FtpWebRequest request;
		private FtpWebResponse response;
		private Stream stream;
		private int bufferSize = 2048;

		public FTPManager(string[] FTPSettings)
		{
			host = FTPSettings[0];
			username = FTPSettings[1];
			password = FTPSettings[2];
		}
		
		private void InitiateConnection(string type)
		{
			try
			{
				request = (FtpWebRequest)WebRequest.Create(type);
				request.Credentials = new NetworkCredential(username, password);

				request.KeepAlive = true;
				request.UseBinary = true;
				request.UsePassive = true;
			}
			catch (Exception e)
			{
				File.AppendAllText(MainForm.file, string.Format("{0}{0}FTP Error - ({2}) - {1}{0}", nl, System.DateTime.Now, e.ToString()));
			}
		}

		public void UploadFile(string remoteFile, string localFile)
		{
			try
			{
				InitiateConnection(host + remoteFile);

				request.Method = WebRequestMethods.Ftp.UploadFile;
				stream = request.GetRequestStream();

				FileStream fileStream = new FileStream(localFile, FileMode.Open);
				byte[] byteBuffer = new byte[bufferSize];
				int bytesSent = fileStream.Read(byteBuffer, 0, bufferSize);

				try
				{
					while (bytesSent != 0)
					{
						stream.Write(byteBuffer, 0, bytesSent);
						bytesSent = fileStream.Read(byteBuffer, 0, bufferSize);
					}
				}
				catch (Exception ex)
				{
					File.AppendAllText(MainForm.file, string.Format("{0}{0}FTP Error - ({2}) - {1}{0}", nl, System.DateTime.Now, ex.ToString()));
				}

				fileStream.Close();
				stream.Close();
				request = null;
			}
			catch (Exception e)
			{
				File.AppendAllText(MainForm.file, string.Format("{0}{0}FTP Error - ({2}) - {1}{0}", nl, System.DateTime.Now, e.ToString()));
			}
		}

		public void DownloadFile(string remoteFile, string localFile)
		{
			try
			{
				InitiateConnection(host + remoteFile);

				request.Method = WebRequestMethods.Ftp.DownloadFile;
				response = (FtpWebResponse)request.GetResponse();
				stream = response.GetResponseStream();

				FileStream fileStream = new FileStream(localFile, FileMode.Create);
				byte[] byteBuffer = new byte[bufferSize];
				int bytesRead = fileStream.Read(byteBuffer, 0, bufferSize);

				try
				{
					while (bytesRead > 0)
					{
						fileStream.Write(byteBuffer, 0, bytesRead);
						bytesRead = stream.Read(byteBuffer, 0, bufferSize);
					}
				}
				catch (Exception e)
				{
					File.AppendAllText(MainForm.file, string.Format("{0}{0}FTP Error - ({2}) - {1}{0}", nl, System.DateTime.Now, e.ToString()));
				}

				fileStream.Close();
				stream.Close();
				response.Close();
				request = null;
			}
			catch (Exception e)
			{
				File.AppendAllText(MainForm.file, string.Format("{0}{0}FTP Error - ({2}) - {1}{0}", nl, System.DateTime.Now, e.ToString()));
			}
		}

		public void CreateDirectory(string newDirectory)
		{
			try
			{
				InitiateConnection(host + newDirectory);

				request.Method = WebRequestMethods.Ftp.MakeDirectory;
				response = (FtpWebResponse)request.GetResponse();

				response.Close();
				request = null;
			}
			catch (Exception e)
			{
				File.AppendAllText(MainForm.file, string.Format("{0}{0}FTP Error - ({2}) - {1}{0}", nl, System.DateTime.Now, e.ToString()));
			}
		}

		public string ReadFile(string remoteFile)
		{
			try
			{
				InitiateConnection(host + remoteFile);

				request.Method = WebRequestMethods.Ftp.DownloadFile;
				response = (FtpWebResponse)request.GetResponse();
				stream = response.GetResponseStream();
				
				StreamReader reader = new StreamReader(stream);
				string result = reader.ReadToEnd();

				response.Close();
				stream.Close();
				request = null;

				return result;
			}
			catch (Exception e)
			{
				File.AppendAllText(MainForm.file, string.Format("{0}{0}FTP Error - ({2}) - {1}{0}", nl, System.DateTime.Now, e.ToString()));
				return "read failed";
			}
		}

		public void DeleteFile(string remoteFile)
		{
			try
			{
				InitiateConnection(host + remoteFile);

				request.Method = WebRequestMethods.Ftp.DeleteFile;
				response = (FtpWebResponse)request.GetResponse();
				
				response.Close();
				request = null;
			}
			catch (Exception e)
			{
				File.AppendAllText(MainForm.file, string.Format("{0}{0}FTP Error - ({2}) - {1}{0}", nl, System.DateTime.Now, e.ToString()));
			}
		}
	}
}
