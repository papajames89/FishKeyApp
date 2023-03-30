using System.Net;

namespace FishKeyApp.Controllers
{
    public class FtpController
    {
        private readonly string _cacheDir = FileSystem.Current.CacheDirectory;
        private readonly string _localFile;
        private readonly string _ftpUrl = "ftp://185.27.134.11/htdocs/{0}/{1}";
        private readonly string _ftpUsername = "epiz_33886186";
        private readonly string _ftpPassword = "mnFbku48yFDR";
        private readonly string _imgForlder = "img";
        private readonly string _mp3Forlder = "mp3";
        public FtpController()
        {
            _localFile = _cacheDir + "\\{0}";
        }
        public string DownloadImgFile(string fileName)
        {
            return DownloadFile(_imgForlder, fileName);
        }
        public string DownloadMp3File(string fileName)
        {
            return DownloadFile(_mp3Forlder, fileName);
        }
        private string DownloadFile(string folder, string fileName)
        {
            if (File.Exists(string.Format(_localFile, fileName)))
            {
                return string.Format(_localFile, fileName);
            }
            try
            {
                using (WebClient request = new WebClient())
                {
                    request.Credentials = new NetworkCredential(_ftpUsername, _ftpPassword);
                    byte[] fileData = request.DownloadData(string.Format(_ftpUrl, folder, fileName));

                    using (FileStream file = File.Create(string.Format(_localFile, fileName)))
                    {
                        file.Write(fileData, 0, fileData.Length);
                        file.Close();
                    }
                }
            }
            catch (Exception e)
            {
            }
            return string.Format(_localFile, fileName);
        }
    }
}
