using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

namespace TK.Common
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName";
            name = name.Replace("\"", string.Empty);
            //name = (Guid.NewGuid()).ToString() +System.IO.Path.GetExtension(name); //this is here because Chrome submits files in quotation marks which get treated as part of the filename and get escaped

            name = Path.GetRandomFileName().Replace(".", string.Empty) + Path.GetExtension(name); //this is here because Chrome submits files in quotation marks which get treated as part of the filename and get escaped

            return name;
        }
        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {

            // following line handles other form fields other than files.
            if (String.IsNullOrEmpty(headers.ContentDisposition.FileName)) return base.GetStream(parent, headers);

            // restrict what filetypes can be uploaded
            List<string> extensions = new List<string> { "png", "gif",
                "jpg", "jpeg", "tiff", "pdf", "tif", "bmp","doc","docx","ods","xls","odt","csv","txt","rtf" };
            var filename = headers.ContentDisposition.FileName.Replace("\"", string.Empty); // correct for chrome.

            //make sure it has an extension
            if (filename.IndexOf('.') < 0)
            {
                return Stream.Null;
            }

            //get the extension
            var extension = filename.Split('.').Last();

            //Return stream if match otherwise return null stream.
            return extensions.Contains(extension) ? base.GetStream(parent, headers) : Stream.Null;
        }
    }
}
