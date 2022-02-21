using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public class DbInitializer
    {

        public static async Task Initialize(BMContext context)
        {
            if (context.Countries.Any()) return;

            var Countries = CreateCountries();
            context.Countries.AddRange(Countries);
            await context.SaveChangesAsync();
        }


        private static List<Country> CreateCountries()
        {
            var Countries = new List<Country>(){
                new Country { Title= "Argentina", Abbr = "AR", FlagImageUrl = "Images/Flags/AR.jpg" },
                new Country { Title= "Australia", Abbr = "AU", FlagImageUrl = "Images/Flags/AU.jpg" },
                new Country { Title= "Austria", Abbr = "AT", FlagImageUrl = "Images/Flags/AT.jpg" },
                new Country { Title= "Bahamas", Abbr = "BS", FlagImageUrl = "Images/Flags/BS.jpg" },
                new Country { Title= "Bahrain", Abbr = "BH", FlagImageUrl = "Images/Flags/BH.jpg" },
                new Country { Title= "Bangladesh", Abbr = "BD", FlagImageUrl = "Images/Flags/BD.jpg" },
                new Country { Title= "Barbados", Abbr = "BB", FlagImageUrl = "Images/Flags/BB.jpg" },
                new Country { Title= "Belgium", Abbr = "BE", FlagImageUrl = "Images/Flags/BE.jpg" },
                new Country { Title= "Belize", Abbr = "BZ", FlagImageUrl = "Images/Flags/BZ.jpg" },
                new Country { Title= "Benin", Abbr = "BJ", FlagImageUrl = "Images/Flags/BJ.jpg" },
                new Country { Title= "Bermuda", Abbr = "BM", FlagImageUrl = "Images/Flags/BM.jpg" },
                new Country { Title= "Bolivia", Abbr = "BO", FlagImageUrl = "Images/Flags/BO.jpg" },
                new Country { Title= "Brazil", Abbr = "BR", FlagImageUrl = "Images/Flags/BR.jpg" },
                new Country { Title= "Bulgaria", Abbr = "BG", FlagImageUrl = "Images/Flags/BG.jpg" },
                new Country { Title= "Burkina Faso", Abbr = "BF", FlagImageUrl = "Images/Flags/BF.jpg" },
                new Country { Title= "Chile", Abbr = "CL", FlagImageUrl = "Images/Flags/CL.jpg" },
                new Country { Title= "China", Abbr = "CN", FlagImageUrl = "Images/Flags/CN.jpg" },
                new Country { Title= "Colombia", Abbr = "CO", FlagImageUrl = "Images/Flags/CO.jpg" },
                new Country { Title= "Costa Rica", Abbr = "CR", FlagImageUrl = "Images/Flags/CR.jpg" },
                new Country { Title= "Côte D' Ivoire", Abbr = "CI", FlagImageUrl = "Images/Flags/CI.jpg" },
                new Country { Title= "Croatia", Abbr = "HR", FlagImageUrl = "Images/Flags/HR.jpg" },
                new Country { Title= "Cuba", Abbr = "CU", FlagImageUrl = "Images/Flags/CU.jpg" },
                new Country { Title= "Cyprus", Abbr = "CY", FlagImageUrl = "Images/Flags/CY.jpg" },
                new Country { Title= "Czech Republic", Abbr = "CZ", FlagImageUrl = "Images/Flags/CZ.jpg" },
                new Country { Title= "Denmark", Abbr = "DK", FlagImageUrl = "Images/Flags/DK.jpg" },
                new Country { Title= "Dominican Republic", Abbr = "DO", FlagImageUrl = "Images/Flags/DO.jpg" },
                new Country { Title= "Ecuador", Abbr = "EC", FlagImageUrl = "Images/Flags/EC.jpg" },
                new Country { Title= "Egypt", Abbr = "EG", FlagImageUrl = "Images/Flags/EG.jpg" },
                new Country { Title= "El Salvador", Abbr = "SV", FlagImageUrl = "Images/Flags/SV.jpg" },
                new Country { Title= "Estonia", Abbr = "EE", FlagImageUrl = "Images/Flags/EE.jpg" },
                new Country { Title= "Ethiopia", Abbr = "ET", FlagImageUrl = "Images/Flags/ET.jpg" },
                new Country { Title= "Finland", Abbr = "FI", FlagImageUrl = "Images/Flags/FI.jpg" },
                new Country { Title= "France", Abbr = "FR", FlagImageUrl = "Images/Flags/FR.jpg" },
                new Country { Title= "Gambia", Abbr = "GM", FlagImageUrl = "Images/Flags/GM.jpg" },
                new Country { Title= "Germany", Abbr = "DE", FlagImageUrl = "Images/Flags/DE.jpg" },
                new Country { Title= "Ghana", Abbr = "GH", FlagImageUrl = "Images/Flags/GH.jpg" },
                new Country { Title= "Great Britain [UK]", Abbr = "GB", FlagImageUrl = "Images/Flags/GB.jpg" },
                new Country { Title= "Greece", Abbr = "GR", FlagImageUrl = "Images/Flags/GR.jpg" },
                new Country { Title= "Guatemala", Abbr = "GT", FlagImageUrl = "Images/Flags/GT.jpg" },
                new Country { Title= "Guinea", Abbr = "GN", FlagImageUrl = "Images/Flags/GN.jpg" },
                new Country { Title= "Guyana", Abbr = "GY", FlagImageUrl = "Images/Flags/GY.jpg" },
                new Country { Title= "Honduras", Abbr = "HN", FlagImageUrl = "Images/Flags/HN.jpg" },
                new Country { Title= "Hong Kong", Abbr = "HK", FlagImageUrl = "Images/Flags/HK.jpg" },
                new Country { Title= "Hungary", Abbr = "HU", FlagImageUrl = "Images/Flags/HU.jpg" },
                new Country { Title= "Iceland", Abbr = "IS", FlagImageUrl = "Images/Flags/IS.jpg" },
                new Country { Title= "India", Abbr = "IN", FlagImageUrl = "Images/Flags/IN.jpg" },
                new Country { Title= "Indonesia", Abbr = "ID", FlagImageUrl = "Images/Flags/ID.jpg" },
                new Country { Title= "Iran", Abbr = "IR", FlagImageUrl = "Images/Flags/IR.jpg" },
                new Country { Title= "Iraq", Abbr = "IQ", FlagImageUrl = "Images/Flags/IQ.jpg" },
                new Country { Title= "Ireland", Abbr = "IE", FlagImageUrl = "Images/Flags/IE.jpg" },
                new Country { Title= "Israel", Abbr = "IL", FlagImageUrl = "Images/Flags/IL.jpg" },
                new Country { Title= "Italy", Abbr = "IT", FlagImageUrl = "Images/Flags/IT.jpg" },
                new Country { Title= "Jamaica", Abbr = "JM", FlagImageUrl = "Images/Flags/JM.jpg" },
                new Country { Title= "Japan", Abbr = "JP", FlagImageUrl = "Images/Flags/JP.jpg" },
                new Country { Title= "Jordan", Abbr = "JO", FlagImageUrl = "Images/Flags/JO.jpg" },
                new Country { Title= "Kenya", Abbr = "KE", FlagImageUrl = "Images/Flags/KE.jpg" },
                new Country { Title= "Kuwait", Abbr = "KW", FlagImageUrl = "Images/Flags/KW.jpg" },
                new Country { Title= "Latvia", Abbr = "LV", FlagImageUrl = "Images/Flags/LV.jpg" },
                new Country { Title= "Lebanon", Abbr = "LB", FlagImageUrl = "Images/Flags/LB.jpg" },
                new Country { Title= "Liberia", Abbr = "LR", FlagImageUrl = "Images/Flags/LR.jpg" },
                new Country { Title= "Libya", Abbr = "LY", FlagImageUrl = "Images/Flags/LY.jpg" },
                new Country { Title= "Liechtenstein", Abbr = "LI", FlagImageUrl = "Images/Flags/LI.jpg" },
                new Country { Title= "Lithuania", Abbr = "LT", FlagImageUrl = "Images/Flags/LT.jpg" },
                new Country { Title= "Luxembourg", Abbr = "LU", FlagImageUrl = "Images/Flags/LU.jpg" },
                new Country { Title= "Malawi", Abbr = "MW", FlagImageUrl = "Images/Flags/MW.jpg" },
                new Country { Title= "Malaysia", Abbr = "MY", FlagImageUrl = "Images/Flags/MY.jpg" },
                new Country { Title= "Mali", Abbr = "ML", FlagImageUrl = "Images/Flags/ML.jpg" },
                new Country { Title= "Malta", Abbr = "MT", FlagImageUrl = "Images/Flags/MT.jpg" },
                new Country { Title= "Mauritania", Abbr = "MR", FlagImageUrl = "Images/Flags/MR.jpg" },
                new Country { Title= "Mauritius", Abbr = "MU", FlagImageUrl = "Images/Flags/MU.jpg" },
                new Country { Title= "Mexico", Abbr = "MX", FlagImageUrl = "Images/Flags/MX.jpg" },
                new Country { Title= "Morocco", Abbr = "MA", FlagImageUrl = "Images/Flags/MA.jpg" },
                new Country { Title= "Netherlands", Abbr = "NL", FlagImageUrl = "Images/Flags/NL.jpg" },
                new Country { Title= "New Zealand", Abbr = "NZ", FlagImageUrl = "Images/Flags/NZ.jpg" },
                new Country { Title= "Nicaragua", Abbr = "NI", FlagImageUrl = "Images/Flags/NI.jpg" },
                new Country { Title= "Niger", Abbr = "NE", FlagImageUrl = "Images/Flags/NE.jpg" },
                new Country { Title= "Nigeria", Abbr = "NG", FlagImageUrl = "Images/Flags/NG.jpg" },
                new Country { Title= "Norway", Abbr = "NO", FlagImageUrl = "Images/Flags/NO.jpg" },
                new Country { Title= "Oman", Abbr = "OM", FlagImageUrl = "Images/Flags/OM.jpg" },
                new Country { Title= "Pakistan", Abbr = "PK", FlagImageUrl = "Images/Flags/PK.jpg" },
                new Country { Title= "Panama", Abbr = "PA", FlagImageUrl = "Images/Flags/PA.jpg" },
                new Country { Title= "Paraguay", Abbr = "PY", FlagImageUrl = "Images/Flags/PY.jpg" },
                new Country { Title= "Peru", Abbr = "PE", FlagImageUrl = "Images/Flags/PE.jpg" },
                new Country { Title= "Philippines", Abbr = "PH", FlagImageUrl = "Images/Flags/PH.jpg" },
                new Country { Title= "Poland", Abbr = "PL", FlagImageUrl = "Images/Flags/PL.jpg" },
                new Country { Title= "Portugal", Abbr = "PT", FlagImageUrl = "Images/Flags/PT.jpg" },
                new Country { Title= "Puerto Rico", Abbr = "PR", FlagImageUrl = "Images/Flags/PR.jpg" },
                new Country { Title= "Qatar", Abbr = "QA", FlagImageUrl = "Images/Flags/QA.jpg" },
                new Country { Title= "Republic of Korea", Abbr = "KR", FlagImageUrl = "Images/Flags/KR.jpg" },
                new Country { Title= "Romania", Abbr = "RO", FlagImageUrl = "Images/Flags/RO.jpg" },
                new Country { Title= "Russian Federation", Abbr = "RU", FlagImageUrl = "Images/Flags/RU.jpg" },
                new Country { Title= "Saudi Arabia", Abbr = "SA", FlagImageUrl = "Images/Flags/SA.jpg" },
                new Country { Title= "Senegal", Abbr = "SN", FlagImageUrl = "Images/Flags/SN.jpg" },
                new Country { Title= "Seychelles", Abbr = "SC", FlagImageUrl = "Images/Flags/SC.jpg" },
                new Country { Title= "Sierra Leone", Abbr = "SL", FlagImageUrl = "Images/Flags/SL.jpg" },
                new Country { Title= "Singapore", Abbr = "SG", FlagImageUrl = "Images/Flags/SG.jpg" },
                new Country { Title= "Slovakia", Abbr = "SK", FlagImageUrl = "Images/Flags/SK.jpg" },
                new Country { Title= "Slovenia", Abbr = "SI", FlagImageUrl = "Images/Flags/SI.jpg" },
                new Country { Title= "South Africa", Abbr = "ZA", FlagImageUrl = "Images/Flags/ZA.jpg" },
                new Country { Title= "Spain", Abbr = "ES", FlagImageUrl = "Images/Flags/ES.jpg" },
                new Country { Title= "Sri Lanka", Abbr = "LK", FlagImageUrl = "Images/Flags/LK.jpg" },
                new Country { Title= "Sudan", Abbr = "SD", FlagImageUrl = "Images/Flags/SD.jpg" },
                new Country { Title= "Surinam", Abbr = "SR", FlagImageUrl = "Images/Flags/SR.jpg" },
                new Country { Title= "Sweden", Abbr = "SE", FlagImageUrl = "Images/Flags/SE.jpg" },
                new Country { Title= "Switzerland", Abbr = "CH", FlagImageUrl = "Images/Flags/CH.jpg" },
                new Country { Title= "Syria", Abbr = "SY", FlagImageUrl = "Images/Flags/SY.jpg" },
                new Country { Title= "Syrian Arab Republic", Abbr = "SY", FlagImageUrl = "Images/Flags/SY.jpg" },
                new Country { Title= "Taiwan, Province of China", Abbr = "TW", FlagImageUrl = "Images/Flags/TW.jpg" },
                new Country { Title= "Tanzania", Abbr = "TZ", FlagImageUrl = "Images/Flags/TZ.jpg" },
                new Country { Title= "Thailand", Abbr = "TH", FlagImageUrl = "Images/Flags/TH.jpg" },
                new Country { Title= "Trinidad and Tobago", Abbr = "TT", FlagImageUrl = "Images/Flags/TT.jpg" },
                new Country { Title= "Tunisia", Abbr = "TN", FlagImageUrl = "Images/Flags/TN.jpg" },
                new Country { Title= "Turkey", Abbr = "TR", FlagImageUrl = "Images/Flags/TR.jpg" },
                new Country { Title= "Uganda", Abbr = "UG", FlagImageUrl = "Images/Flags/UG.jpg" },
                new Country { Title= "Ukraine", Abbr = "UA", FlagImageUrl = "Images/Flags/UA.jpg" },
                new Country { Title= "United Arab Emirates", Abbr = "AE", FlagImageUrl = "Images/Flags/AE.jpg" },
                new Country { Title= "Uruguay", Abbr = "UY", FlagImageUrl = "Images/Flags/UY.jpg" },
                new Country { Title= "Venezuela", Abbr = "VE", FlagImageUrl = "Images/Flags/VE.jpg" },
                new Country { Title= "Vietnam", Abbr = "VN", FlagImageUrl = "Images/Flags/VN.jpg" },
                new Country { Title= "Yemen", Abbr = "YE", FlagImageUrl = "Images/Flags/YE.jpg" },
                new Country { Title= "Zambia", Abbr = "ZM", FlagImageUrl = "Images/Flags/ZM.jpg" },
                new Country { Title= "Zimbabwe", Abbr = "ZW", FlagImageUrl = "Images/Flags/ZW.jpg" },

            };

            return Countries;
        }
    
           }
}