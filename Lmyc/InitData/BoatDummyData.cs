using Lmyc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMYCWebsite.Data
{
    public class BoatDummyData
    {
        public static System.Collections.Generic.List<Boat> getBoats()
        {
            List<Boat> boats = new List<Boat>()
            {
                new Boat()
                {
                    BoatId = "1",
                    BoatName = "Sharqui",
                    BoatStatus = BoatStatus.Operational,
                    BoatPicture = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    BoatDescription = "Sharqui was added to the fleet in 2016.  Another of the very popular C&C designs for style, " +
                                       "comfort, and speed. Sharqui sleeps five comfortably, has an aftermarket outboard motor, and sports" +
                                       "a very generous dodger for protection on heavy weather days.",
                    BoatLength = 27,
                    BoatMake = "C&C",
                    BoatYear = 1981,
                    CreditsPerHourOfUsage = 100

                },
                new Boat()
                {
                    BoatId = "2",
                    BoatName = "Pegasus",
                    BoatStatus = BoatStatus.Operational,
                    BoatPicture = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    BoatDescription = "Pegasus will be oufitted for travelling to Desolation Sound for the first time this summer. Members are " +
                    "looking forward to a roomier more comfortable boat with generous side decks.",
                    BoatLength = 27,
                    BoatMake = "C&C",
                    BoatYear = 1979,
                    CreditsPerHourOfUsage = 100

                },
                new Boat()
                {
                    BoatId = "3",
                    BoatName = "Lightcure",
                    BoatStatus = BoatStatus.Operational,
                    BoatPicture = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    BoatDescription = "She is one of our most popular boats, being a good sailor and comfortable while cruising. She sleeps " +
                    "5 adults comfortably.She was refitted in 2005 and is powered by a remote controlled Yamaha outboard." +
                    " Lightcure has a BBQ, cockpit table, asymmetrical spinnaker and all the extras to be comfortable for" +
                    " cruising.She is also rigged for use in local sailboat races.",
                    BoatLength = 27,
                    BoatMake = "C&C Mark 3",
                    BoatYear = 1979,
                    CreditsPerHourOfUsage = 100

                },
                new Boat()
                {
                    BoatId = "4",
                    BoatName = "Frankie",
                    BoatStatus = BoatStatus.Operational,
                    BoatPicture = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    BoatDescription = "She is designated as a “day sailor”, and is available for use in Semiahmoo Bay. " +
                    "She is outfitted with some of the amenities for cruising and may be used occasionally for overnight trips." +
                    "She might sleep 4 adults comfortably.Frankie has a spray dodger and is powered by a Yamaha outboard.`",
                    BoatLength = 25,
                    BoatMake = "'Cal Mark 2'",
                    BoatYear = 1983,
                    CreditsPerHourOfUsage = 100

                },
                new Boat()
                {
                    BoatId = "5",
                    BoatName = "White Swan",
                    BoatStatus = BoatStatus.Operational,
                    BoatPicture = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    BoatDescription = "She is a cruising boat, with a spray dodger, inboard diesel engine and enclosed head." +
                    " White Swan is popular for longer trips to the local islands.She sleeps 4 adults very" +
                    " comfortably with a private aft cabin and V - berth",
                    BoatLength = 28,
                    BoatMake = "MkII",
                    BoatYear = 1979,
                    CreditsPerHourOfUsage = 100

                },
                new Boat()
                {
                    BoatId = "6",
                    BoatName = "Peak Time",
                    BoatStatus = BoatStatus.Operational,
                    BoatPicture = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    BoatDescription = "She has a spray dodger, BBQ and a comfortable cockpit." +
                    " She has all the amenities and can be used as a cruiser or day sailing boat." +
                    " She can sleep 4 adults.Peak Time is powered by a Yamaha outboard engine." +
                    " She is also rigged for use in local sailboat races",
                    BoatLength = 27,
                    BoatMake = "C&C Mark 5",
                    BoatYear = 1985,
                    CreditsPerHourOfUsage = 100

                },
                new Boat()
                {
                    BoatId = "6",
                    BoatName = "Pegasus",
                    BoatStatus = BoatStatus.Operational,
                    BoatPicture = new Byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                    BoatDescription = "A spacious fast cruiser She has a comfortable cockpit, spray dodger." +
                    " She has all the amenities of a cruiser Large aft head/shower" +
                    " She can sleep up to 6 adults in comfort Powered by a Yanmar diesel." +
                    " Stable wing keel design Open transom with swim grid, BBQ for sailing adventures" +
                    " She is a cruising boat, with a spray dodger, inboard diesel engine and enclosed head." +
                    " White Swan is popular for longer trips to the local islands.She sleeps 4 adults very" +
                    " comfortably with a private aft cabin and V-berth",
                    BoatLength = 30,
                    BoatMake = "Cruiser",
                    BoatYear = 1979,
                    CreditsPerHourOfUsage = 100

                },

            };
            return boats;
        }
    }
}