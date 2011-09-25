using System;
using System.Collections.Generic;

namespace Fleck
{
  public class WebSocketHttpRequest
  {
    IDictionary<string,string> headers = new Dictionary<string,string>();

    public string Method { get; set; }

    public string Path { get; set; }
    
    public string Body { get; set; }
    
    public byte[] Bytes { get; set; }
    
    public string this[string name]
    {
        get {
                string value;
                return headers.TryGetValue(name, out value) ? value : default(string);
            }
    }
    
    public IDictionary<string,string> Headers {
      get {
        return headers;
      }
    }
    
  }
}

