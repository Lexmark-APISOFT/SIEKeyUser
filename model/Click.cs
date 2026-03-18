using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Media;

namespace SIE_KEY_USER.model
{
    public class Click
    {
        public static void playSimpleSound()
        {
            //SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\jzamorabello\Documents\Visual Studio 2012\Projects\Proyecto SIE\SIE\SIE\Content\Cick.wav");
            //SoundPlayer simpleSound = new SoundPlayer(@"E:\Development\jzamorabello\SIE\Content\Cick.wav");
            string path = HttpContext.Current.Server.MapPath(@"~\Virtual\Cick.wav");
            SoundPlayer simpleSound = new SoundPlayer(path);
            simpleSound.Load();
            simpleSound.Play();
        }
    }
}