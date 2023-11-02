using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraClone.utils
{
    public class ConsoleContentBuilder
    {
        private static readonly string logo = "\r\n _____                            ___         __     \r\n/\\___ \\  __                     /'___`\\     /'__`\\   \r\n\\/__/\\ \\/\\_\\  _ __    __       /\\_\\ /\\ \\   /\\ \\/\\ \\  \r\n   _\\ \\ \\/\\ \\/\\`'__\\/'__`\\     \\/_/// /__  \\ \\ \\ \\ \\ \r\n  /\\ \\_\\ \\ \\ \\ \\ \\//\\ \\L\\.\\_      // /_\\ \\__\\ \\ \\_\\ \\\r\n  \\ \\____/\\ \\_\\ \\_\\\\ \\__/.\\_\\    /\\______/\\_\\\\ \\____/\r\n   \\/___/  \\/_/\\/_/ \\/__/\\/_/    \\/_____/\\/_/ \\/___/ \r\n                                                     \r\n                                                     \r\n";
        private bool includeLogo = false;

        override public string ToString()
        {
            StringBuilder content = new();

            if(includeLogo)
                content.Append(logo + "\n");

            return content.ToString();
        }

        public class Builder
        {
            private readonly ConsoleContentBuilder content;

            public Builder()
            {
                content = new();
            }

            public Builder IncludeLogo()
            {
                content.includeLogo = true;
                return this;
            }

            public ConsoleContentBuilder Build()
            {
                return content;
            }
        }
    }
}
