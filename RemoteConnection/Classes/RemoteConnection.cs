using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.RemoteConnection
{
    public class RemoteConnection
    {
        private ScpClient m_scp;
        private SshClient m_ssh;

        public RemoteConnection(ConnectionCredentialsDTO credentials)
        {
            m_scp = new ScpClient(credentials.URI, credentials.Username, credentials.Password);
            m_ssh = new SshClient(credentials.URI, credentials.Username, credentials.Password);
        }

        public string DownloadFile(string fileName, string destination)
        {
            string log = string.Empty;

            FileStream strm = new FileStream(destination, FileMode.Create);
            ConnectSCP();
            m_scp.Download(fileName, strm);
            strm.Close();

            return log;
        }

        public string UploadFile(string filename, string destination)
        {
            string log = string.Empty;

            ConnectSCP();
            FileInfo fi = new FileInfo(filename);
            m_scp.Upload(fi, destination);

            return log;
        }

        public string SubmitSSHCommand(string command)
        {
            string log = string.Empty;

            ConnectSSH();
            log = m_ssh.RunCommand(command).Result;

            return log;
        }

        public async Task<string> SubmitAsyncSSHCommand(string command)
        {
            string log = string.Empty;

            log = await Task.Run(() => { ConnectSSH(); return m_ssh.RunCommand(command).Result; });

            return log;
        }

        public void ConnectSCP()
        {
            if (!m_scp.IsConnected)
            {
                m_scp.Connect();
            }
        }

        public void DisconnectSCP()
        {
            if (m_scp.IsConnected)
            {
                m_scp.Disconnect();
            }
        }

        public void ConnectSSH()
        {
            if (!m_ssh.IsConnected)
            {
                m_ssh.Connect();
            }
        }

        public void DisconnectSSH()
        {
            if (m_ssh.IsConnected)
            {
                m_ssh.Disconnect();
            }
        }
    }
}
