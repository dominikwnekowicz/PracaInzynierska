using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PwszAlarm.Model
{
    public class EscapeRoutes
    {
        List<EscapeRoutes> escapeRoutes;
        public int Id { get; set; }
        public string FloorName { get; set; }
        public string Side { get; set; }
        public string Text { get; set; }
        public int ImageId { get; set; }

        public EscapeRoutes()
        {
            
        }

        public List<EscapeRoutes> GetEscapeRoutes(string side)
        {
            escapeRoutes = new List<EscapeRoutes>
            {
                //Front
                new EscapeRoutes {Id=1, FloorName="Parter", Side="front", Text="Udaj się do jednego z trzech wyjść:\n-Obok sklepiku\n-Głównego\n-Obok windy", ImageId = Resource.Drawable.ParterWyjscia },
                new EscapeRoutes {Id=2, FloorName="Pierwsze piętro", Side="front", Text="Idź schodami na dół.\nNIE UŻYWAJ WINDY!!!", ImageId = Resource.Drawable.PierwszePietroSchodyPrzod},
                new EscapeRoutes {Id=3, FloorName="Drugie piętro", Side="front", Text="Zejdź schodami.\nNIE UŻYWAJ WINDY!!!", ImageId = Resource.Drawable.DrugiePietroSchodyPrzod},

                //Back
                new EscapeRoutes {Id=4, FloorName="Parter", Side="back", Text="Udaj się do wyjścia pod schodami.", ImageId = Resource.Drawable.WyjscieTyl},
                new EscapeRoutes {Id=5, FloorName="Pierwsze piętro", Side="back", Text="Idź schodami na dół.", ImageId = Resource.Drawable.PierwszePietroSchodyTyl},
                new EscapeRoutes {Id=6, FloorName="Drugie piętro", Side="back", Text="Zejdź schodami.", ImageId = Resource.Drawable.DrugiePietroSchodyTyl},

                //Main
                new EscapeRoutes {Id=7, FloorName="Parter", Side="seven", Text="Udaj się do jednego z dwóch wyjść w sali.", ImageId = Resource.Drawable.Sala07},
                new EscapeRoutes {Id=8, FloorName="Parter", Side="main", Text="Udaj się do wyjścia głównego.", ImageId = Resource.Drawable.GlowneWyjscie},


            };
            return escapeRoutes.Where(e => e.Side == side).ToList();
        }
    }
}