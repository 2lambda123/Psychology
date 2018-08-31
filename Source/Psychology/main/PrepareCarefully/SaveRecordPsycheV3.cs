﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RimWorld;
using Verse;

namespace Psychology.PrepareCarefully
{
    public class SaveRecordPsycheV3 : IExposable
    {
        public List<PersonalityNode> nodes = new List<PersonalityNode>();
        public int upbringing;
        public float sexDrive = 1f;
        public float romanticDrive = 1f;
        public int kinseyRating = 0;

        public SaveRecordPsycheV3()
        {
        }

        public SaveRecordPsycheV3(Pawn pawn)
        {
            if(PsycheHelper.PsychologyEnabled(pawn))
            {
                nodes = PsycheHelper.Comp(pawn).Psyche.PersonalityNodes;
                upbringing = PsycheHelper.Comp(pawn).Psyche.upbringing;
                if(PsychologyBase.ActivateKinsey())
                {
                    sexDrive = PsycheHelper.Comp(pawn).Sexuality.sexDrive;
                    romanticDrive = PsycheHelper.Comp(pawn).Sexuality.romanticDrive;
                    kinseyRating = PsycheHelper.Comp(pawn).Sexuality.kinseyRating;
                }
            }
        }

        public void ExposeData()
        {
            if (Scribe.mode == LoadSaveMode.Saving || Scribe.loader.curXmlParent["personality"] != null)
            {
                Scribe_Collections.Look(ref nodes, "personality", LookMode.Deep, null);
            }
            Scribe_Values.Look(ref upbringing, "upbringing");
            if(PsychologyBase.ActivateKinsey())
            {
                if (Scribe.mode == LoadSaveMode.Saving || Scribe.loader.curXmlParent["sexDrive"] != null)
                {
                    Scribe_Values.Look(ref sexDrive, "sexDrive");
                }
                if (Scribe.mode == LoadSaveMode.Saving || Scribe.loader.curXmlParent["romanticDrive"] != null)
                {
                    Scribe_Values.Look(ref romanticDrive, "romanticDrive");
                }
                if (Scribe.mode == LoadSaveMode.Saving || Scribe.loader.curXmlParent["kinseyRating"] != null)
                {
                    Scribe_Values.Look(ref kinseyRating, "kinseyRating");
                }
            }
        }
    }
}