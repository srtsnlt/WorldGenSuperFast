using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Terraria;
using Terraria.GameContent.UI.States;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.World.Generation;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.Reflection;
using log4net;
using System.Linq;
using System.IO;

namespace WorldGenSuperFast
{
    class WorldGeneratorRenew {

		internal List<GenPass> _passes = new List<GenPass>();

		internal int _seed;

		internal OrderedDictionary _passesDict;

        public WorldGeneratorRenew(int seed)
        {
            _seed = seed;
        }

		internal List<Exception> currentError = new List<Exception>();

		public List<Exception> GenerateWorld(GenerationProgress progress = null)
		{
			Stopwatch stopwatch = new Stopwatch();
			float _num = 0f;
			ILog iLog = LogManager.GetLogger("WorldGenSuperFast");
			_passesDict=new OrderedDictionary();

			iLog.Debug("Pass List:");
			foreach (GenPass pass in _passes)
			{
				_num += pass.Weight;

				if(!_passesDict.Contains(pass.Name))
				{
					_passesDict.Add(pass.Name,new List<GenPass>());
				}
				((List<GenPass>)_passesDict[pass.Name]).Add(pass);
				iLog.Debug(pass.Name);
			}

			if (progress == null)
			{
				progress = new GenerationProgress();
			}
			progress.TotalWeight = _num;
			Main.menuMode = 888;
			Main.MenuUI.SetState(new UIWorldLoad(progress));

			iLog.Debug("Generation start:");
			try{
				// runParallel( progress, stopwatch,"Parallel", new String[] {"Reset","Terrain"});
				// runParallel( progress, stopwatch,"Parallel", new String[] {"Tunnels","Sand","Mount Caves","Dirt Wall Backgrounds","Rocks In Dirt","Dirt In Rocks","Clay","Small Holes","Dirt Layer Caves","Rock Layer Caves","Surface Caves"});
				// runParallel( progress, stopwatch,"Parallel", new String[] {"Slush Check","Grass","Jungle"});
				// runParallel( progress, stopwatch,"Parallel", new String[] {"Marble","Granite","Mud Caves To Grass","Full Desert","Floating Islands"});
				// runParallel( progress, stopwatch,"Parallel", new String[] {"Mushroom Patches","Dungeon"});
				// runParallel( progress, stopwatch,"Parallel", new String[] {"Mud To Dirt","Silt","Shinies","Thorium Mod: Shinies","EvilIsland","Thorium Mod: Biome Chests","Calamity Mod: Biome Chests","Webs","Underworld","Lakes","Slush","Corruption","Mud Caves To Grass","Beaches","Gems","Thorium Mod: Gem Ores","Gravitating Sand","Clean Up Dirt","Pyramids","Dirt Rock Wall Runner","Living Trees"});
				// runParallel( progress, stopwatch,"Parallel", new String[] {"Wood Tree Walls","Altars","Wet Jungle","Remove Water From Sand","SunkenSea","Jungle Temple","Hives","Jungle Chests","Smooth World","Spider Caves"});
				// runParallel( progress, stopwatch,"Parallel", new String[] {"Settle Liquids","Pots","Hellforge","Spreading Grass"});
				// runParallel( progress, stopwatch,"Parallel", new String[] {"Waterfalls","Ice","Wall Variety","Traps","Life Crystals","Statues","Buried Chests","Surface Chests","Jungle Chests Placement"});
				// runParallel( progress, stopwatch,"Parallel", new String[] {"Moss","Temple","Jungle Trees","Floating Island Houses"});
				// runParallel( progress, stopwatch,"Parallel", new String[] {"Quick Cleanup","Piles","Gem Caves"});
				// runParallel( progress, stopwatch,"Parallel", new String[] {"Water Chests","Moss","Spawn Point","Grass Wall","Guide","Sunflowers","Planting Trees","Herbs","Dye Plants","Webs And Honey","Weeds","Mud Caves To Grass","Jungle Plants","Vines","Flowers","Mushrooms","Stalac","Gems In Ice Biome","Random Gems"});
				// runParallel( progress, stopwatch,"Parallel", new String[] {"Micro Biomes","Ice Walls","Moss Grass","Muds Walls In Jungle","Larva","Settle Liquids Again","Tile Cleanup","Lihzahrd Altars"});
				// runParallel( progress, stopwatch,"Parallel", new String[] {"Final Cleanup"});

				runParallel( progress, stopwatch,"Parallel", new String[] {"Reset","Terrain"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Tunnels","Sand","Mount Caves","Dirt Wall Backgrounds","Rocks In Dirt","Dirt In Rocks","Clay","Small Holes","Dirt Layer Caves","Rock Layer Caves","Surface Caves"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Slush Check","Grass","Jungle"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Marble","Granite","Mud Caves To Grass","Full Desert","Floating Islands"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Mushroom Patches","Dungeon"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Mud To Dirt","Silt","Shinies","Thorium Mod: Shinies","EvilIsland","Thorium Mod: Biome Chests","Calamity Mod: Biome Chests","Webs","Underworld","Lakes","Slush","Corruption","Mud Caves To Grass","Beaches","Gems","Thorium Mod: Gem Ores","Gravitating Sand","Clean Up Dirt","Pyramids","Dirt Rock Wall Runner","Living Trees","Briar"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Wood Tree Walls","Altars","Wet Jungle","Remove Water From Sand","SunkenSea","Jungle Temple","Hives","Jungle Chests","Smooth World","Gem Caves"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Settle Liquids","Pots","Hellforge","Spreading Grass"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Waterfalls","Ice","Wall Variety","Traps","Life Crystals","Statues","Buried Chests","Surface Chests","Jungle Chests Placement","Asteroids"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Moss","Temple","Jungle Trees","Floating Island Houses"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Quick Cleanup","Spider Caves","Piles","Thorium Mod: Gem Piles"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Water Chests","Moss","Spawn Point","Grass Wall","Guide","Sunflowers","Planting Trees","Herbs","Dye Plants","Webs And Honey","Weeds","Mud Caves To Grass","Jungle Plants","Vines","Flowers","Mushrooms","Stalac","Gems In Ice Biome","Random Gems","Thorium Mod: Random Gems","Sulphur"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Micro Biomes","Ice Walls","Moss Grass","Muds Walls In Jungle","Larva"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Settle Liquids Again","Tile Cleanup","Sepulchure","Lihzahrd Altars","Thorium Mod: Pickaxe Shrine"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Thorium Mod: Hot Loot"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Thorium Mod: Bat Cave"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Thorium Mod: Extra Loot","Thorium Mod: Aquatic Depths"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Final Cleanup"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"SpecialShrines","Rust and Dust","Abyss"});
				runParallel( progress, stopwatch,"Parallel", new String[] {"Sulphur2","IWannaRock","BrimstoneCrag","Planetoid Test"});

				runParallel(progress,stopwatch,"Serial",_passes.Select(x => x.Name).ToArray());
				// runParallel(progress,stopwatch,"Serial",new String[] {"Spawn Point"});

				return currentError;
			}
			catch
			{
				return currentError;
			}
		}

		private void runParallel(GenerationProgress progress,Stopwatch stopwatch,String mode,String[] genNameList)
		{
			List<GenPass> genPassList = new List<GenPass>();
			foreach(String genName in genNameList)
			{
				if(_passesDict.Contains(genName))
				{
					GenPass pass=((List<GenPass>)_passesDict[genName])[0];
					genPassList.Add(pass);
					
					((List<GenPass>)_passesDict[genName]).RemoveAt(0);
					_passes.Remove(pass);

					if(((List<GenPass>)_passesDict[genName]).Count == 0)
					{
						_passesDict.Remove(genName);
					}
				}
			}

			try
			{
				if(mode=="Parallel")
				{
					var partitioner = Partitioner.Create<GenPass>(genPassList, EnumerablePartitionerOptions.NoBuffering);
					Parallel.ForEach(partitioner,new ParallelOptions() {  MaxDegreeOfParallelism = 4},pass2 =>
					// Parallel.ForEach(partitioner,pass2 =>
					{
						gen(pass2,progress,stopwatch);
					});
				}
				else
				{
					foreach(GenPass pass3 in genPassList)
					{
						gen(pass3,progress,stopwatch);
					}
				}
			}
			catch (AggregateException ae)
			{
				ILog iLog = LogManager.GetLogger("WorldGenSuperFast");

				foreach (Exception ex in ae.Flatten().InnerExceptions)
				{
					string text = string.Join("\n", Language.GetTextValue("tModLoader.WorldGenError"),ex.Message,ex.InnerException);
					
					// Logging.tML.Error((object)text);
					iLog.Error((object)text);

					// throw ex;
					currentError.Add(ex.InnerException);
				}

				throw ae;
			}
		}

		private void gen(GenPass pass2,GenerationProgress progress,Stopwatch stopwatch)
		{
			ILog iLog = LogManager.GetLogger("WorldGenSuperFast");
			iLog.Debug("Start: "+pass2.Name);
			WorldGen._genRand = new UnifiedRandom(_seed);
			Main.rand = new UnifiedRandom(_seed);
			stopwatch.Start();
			progress.Start(pass2.Weight);
			try{
				pass2.Apply(progress);
			}
			catch(Exception ex)
			{
				throw new Exception(pass2.Name,ex);
			}
			iLog.Debug("Finish: "+pass2.Name);
			progress.End();
			stopwatch.Reset();
		}
    }
}