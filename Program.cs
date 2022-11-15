using System;

namespace crud
{
    class Program
    {
        static void Main(string[] args)
        {

            dbclient dbc = new dbclient();
            dbc.Start();

        }
        
    }
}

