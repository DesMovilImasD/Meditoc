using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CallCenter.Helpers
{

    public class PhoneLadaValidator
    {
        private static List<LadaElement> VALID_LADAS = new List<LadaElement> {
            LadaElement.Create("Acanceh", 988),
            LadaElement.Create("Akil", 997),
            LadaElement.Create("Baca",    991),
            LadaElement.Create("Buctzotz",    991),
            LadaElement.Create("Cacalchen",   991),
            LadaElement.Create("Cansahcab",   991),
            LadaElement.Create("Caucel",  999),
            LadaElement.Create("Celestun",    988),
            LadaElement.Create("Cenotillo",   991),
            LadaElement.Create("Chemax",  985),
            LadaElement.Create("Chicxulub Pueblo",    999),
            LadaElement.Create("Chochola",    988),
            LadaElement.Create("Cholul",  999),
            LadaElement.Create("Colonia Yucatan", 986),
            LadaElement.Create("Conkal",  999),
            LadaElement.Create("Dzidzantun",  991),
            LadaElement.Create("Dzilam de Bravo", 991),
            LadaElement.Create("Dzilam Gonzalez", 991),
            LadaElement.Create("Espita",  986),
            LadaElement.Create("Flamboyanes", 969),
            LadaElement.Create("Halacho", 997),
            LadaElement.Create("Hocaba",  988),
            LadaElement.Create("Hoctun",  988),
            LadaElement.Create("Huhi",    988),
            LadaElement.Create("Hunucma", 988),
            LadaElement.Create("Izamal",  988),
            LadaElement.Create("Kanasin", 999),
            LadaElement.Create("Kantunil",    988),
            LadaElement.Create("Kaua",    985),
            LadaElement.Create("Komchen", 999),
            LadaElement.Create("Las Coloradas",   986),
            LadaElement.Create("Mani",    997),
            LadaElement.Create("Maxcanu", 997),
            LadaElement.Create("Merida",  999),
            LadaElement.Create("Motul",   991),
            LadaElement.Create("Muna",    997),
            LadaElement.Create("Oxkutzcab",   997),
            LadaElement.Create("Panaba",  986),
            LadaElement.Create("Peto",    997),
            LadaElement.Create("Piste",   985),
            LadaElement.Create("Progreso",    969),
            LadaElement.Create("Rio Lagartos",    986),
            LadaElement.Create("San Felipe",  986),
            LadaElement.Create("Seye",    988),
            LadaElement.Create("Sotuta",  988),
            LadaElement.Create("Sucila",  986),
            LadaElement.Create("Tahmek",  988),
            LadaElement.Create("Tecoh",   988),
            LadaElement.Create("Tekanto", 991),
            LadaElement.Create("Tekax",   997),
            LadaElement.Create("Tekit",   997),
            LadaElement.Create("Telchac Pueblo",  991),
            LadaElement.Create("Telchac Puerto",  991),
            LadaElement.Create("Temax",   991),
            LadaElement.Create("Temozon", 985),
            LadaElement.Create("Ticul",   997),
            LadaElement.Create("Tixkokob",    991),
            LadaElement.Create("TTizimin",    986),
            LadaElement.Create("Tunkas",  991),
            LadaElement.Create("Tzucacab",    997),
            LadaElement.Create("Uman",    988),
            LadaElement.Create("Uxmal (Hoteles)", 997),
            LadaElement.Create("Valladolid",  985),
            LadaElement.Create("Xmatkuil",    999),
        };

        public bool isValid(string code)
        {
            try
            {
                if (!Int32.TryParse(code, out int _code)) return false;

                List<int> only_ladas = VALID_LADAS
                    .Select(o => o.Code)
                    .Distinct()
                    .ToList();

                return only_ladas.Contains(_code);
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
    }

    public struct LadaElement
    {
        public string Name { get; set; }
        public int Code { get; set; }

        public static LadaElement Create(string Name, int Code) =>
            new LadaElement(Name: Name, Code: Code);

        public LadaElement(string Name, int Code) 
        {
            this.Code = Code;
            this.Name = Name;
        }
    }
}
