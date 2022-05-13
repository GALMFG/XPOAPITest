using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPOAPITest
{

    public enum SpecialRequirementType
    {
        LFD,
        LFP,
        RSD,
        RSP
    }

    public static class SpecialRequirementTypeExtensions
    {
        public static string GetString(this SpecialRequirementType me)
        {
            switch (me)
            {
                case SpecialRequirementType.LFD:
                    return "LFD: Liftgate Delivery";
                case SpecialRequirementType.LFP:
                    return "LFP: Liftgate Pickup";
                case SpecialRequirementType.RSD:
                    return "RSD: Residential Delivery";
                case SpecialRequirementType.RSP:
                    return "RSP: Residential Pickup";
                default:
                    return "NO VALUE GIVEN";
            }
        }
    }
}
