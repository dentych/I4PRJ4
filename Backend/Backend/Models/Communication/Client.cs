using System;
using System.Net.Sockets;
using Backend.Models.Brains;
using SharedLib.Sockets;

namespace Backend.Models.Communication
{


    /// <summary>
    /// Socket connection client.
    /// </summary>
    public class Client : IClient
    {
        public IError Err = new Error();
        public ISocketConnection Conn = LSC.Connection;

       public bool Connect()
        {
           try
           {
                Conn.Connect(Properties.Settings.Default.CSIP, Properties.Settings.Default.CSPort); // Skal bruge settings.
                return true; // Burde være void.
            }
           catch (Exception)
           {
                
                Err.StdErr("No connection");
               return false;

           }

        }

        public bool Send(string data)
        {
            try
            {
                Conn.Send(data);
                return true;
            }
            catch (Exception)
            {

                Err.StdErr("No connection");
                return false;

            }
        }
    }
}