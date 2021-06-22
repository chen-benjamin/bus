using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Xml;

namespace bus.Model
{
    public class NetworkingManager
    {
        private string url = "http://webservices.nextbus.com/service/publicXMLFeed";
        private HttpClient client = new HttpClient();

        public List<Route> GetRoutes()
        {
            List<Route> routes = new List<Route>();
            XmlTextReader reader = new XmlTextReader(url + "?command=routeList&a=ttc");
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "route") {
                    Route r = new Route();
                    while (reader.MoveToNextAttribute()) {
                        switch (reader.Name) {
                            case "tag":
                                r.Tag = reader.Value;
                                break;
                            case "title":
                                r.Title = reader.Value;
                                break;
                        }
                    }
                    if (r.Tag != "") routes.Add(r);
                }
            }
            return routes;
        }

        public List<Stop> GetStopsByRoute(Route route)
        {
            List<Stop> stops = new List<Stop>();
            XmlTextReader reader = new XmlTextReader(url + "?command=routeConfig&a=ttc&r=" + route.Tag);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "stop" && reader.GetAttribute("title") != null)
                {
                    Stop s = new Stop();
                    s.RouteTag = route.Tag;
                    while (reader.MoveToNextAttribute())
                    {
                        switch (reader.Name)
                        {
                            case "tag":
                                s.Tag = reader.Value;
                                s.Id = reader.Value;
                                break;
                            case "title":
                                s.Title = reader.Value;
                                break;
                        }
                    }
                    if (s.Tag != "") stops.Add(s);
                }
            }
            return stops;
        }

        public List<Bus> GetBusesByStop(Stop s)
        {
            List<Bus> buses = new List<Bus>();
            XmlTextReader reader = new XmlTextReader(url + "?command=predictions&a=ttc&r=" + s.RouteTag + "&s=" + s.Tag);
            int i = 0;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "direction")
                {
                    i++;
                    Bus b = new Bus();
                    b.Title = reader.GetAttribute("title");
                    buses.Add(b);
                }
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "prediction") 
                {
                    if (reader.GetAttribute("minutes") != null) buses[i-1].Times.Add(reader.GetAttribute("minutes"));
                }
            }
            return buses;
        }
    }
}