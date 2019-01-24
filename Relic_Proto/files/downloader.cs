using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;//for webclient
using System.Net.NetworkInformation; //for ping
using System.IO;//for saving files

namespace Relic_Proto
{
    class downloader
    {
        private const String site = "http://www.3y3s.co.uk/files/";
        private const String ip = "216.108.235.205"; //IP address for 3y3s.co.uk
        private const String local = @"C:\project\"; //Must match const in reader


        public void item()
        {
            if (online())
            {
                if (!File.Exists("Content/items.txt"))
                {
                    //The item file has been deleted so it must be downloaded.
                    download(site + "items.txt", "Content/items.txt");
                }
                else
                {
                    if (newVersion(site + "items.txt", "Content/items.txt"))
                    {
                        //a new version is avliable for download
                        download(site + "items.txt", "Content/items.txt");
                    }
                }
            }
            else
            {
                //No connection to 3y3s, do somthing useful
            }
        }

        public void itemOLD()
        {
            if (online())
            {
                if (!File.Exists(local + "items.txt"))
                {
                    //The item file has been deleted so it must be downloaded.
                    download(site + "items.txt", local + "items.txt");
                }
                else
                {
                    if (newVersion(site + "items.txt", local + "items.txt"))
                    {
                        //a new version is avliable for download
                        download(site + "items.txt", local + "items.txt");
                    }
                }
            }
            else
            {
                //No connection to 3y3s, do somthing useful
            }
        }

        public void map()
        {
            if (online())
            {
                if (!File.Exists(local + @"\maps\mapListing.lst"))
                {
                    //The map listing is deleted so it must be downloaded again.
                    download(site + "/maps/mapListing.lst", local + @"maps\mapListing.lst");
                }
                else
                {
                    if (newVersion(site + "/maps/mapListing.lst", local + @"maps\mapListing.lst"))
                    {
                        //a new version is avliable for download
                        download(site + "/maps/mapListing.lst", local + @"maps\mapListing.lst");
                        //Prepare variables need to update each map 
                        bool lineOne;
                        lineOne = true;
                        List<String> listing;
                        listing = new List<String>();
                        StreamReader streamReader;
                        streamReader = new StreamReader(local + @"maps\mapListing.lst");
                        
                        while (!streamReader.EndOfStream)
                        {
                            if (lineOne)
                            {
                                string line = streamReader.ReadLine(); //Read the line and ignore it. Important so that it moves on to the next line. 
                                lineOne = false;
                            }
                            else
                            {
                                string line = streamReader.ReadLine(); //Read the line
                                listing.Add(line);
                            }

                        }
                        streamReader.Close();
                        foreach (String map in listing)
                        {
                            download(site + "/maps/" + map, local + @"maps\" + map);
                        }
                        }
                    }
                }
            else
            {
                //No connection to 3y3s, do somthing useful
            }
        }

        private void download(String address, String path)
        {
            //Downs the file from address and saves it at path. 
            WebClient Client = new WebClient();
            Stream stream = Client.OpenRead(address); //open file
            StreamReader onlineStream = new StreamReader(stream);
            TextWriter fileWriter = new StreamWriter(path);
            while (!onlineStream.EndOfStream)//write each line to a file until the whole file is copied
            {
                string onlineLine = onlineStream.ReadLine();
                fileWriter.WriteLine(onlineLine);
            }
            onlineStream.Close();
            fileWriter.Close();
        }

        private bool online()
        {
            //Pings the server to make sure that there is a conection
            Ping ping = new Ping();
            PingReply conection = ping.Send(IPAddress.Parse(ip));
            if (conection.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool newVersion(String address, String path)
        {
            //Takes the address and path to see if there is a new version aviable online
            WebClient Client = new WebClient();
            Stream stream = Client.OpenRead(address); 
            StreamReader onlineStream = new StreamReader(stream);
            StreamReader fileStream = new StreamReader(path);
            string onlineLine = onlineStream.ReadLine();
            string fileLine = fileStream.ReadLine();
            onlineStream.Close();
            fileStream.Close();
            if (onlineLine != fileLine)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
