using System;
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
// using Terraria.ModLoader.UI;


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

		public void GenerateWorld(GenerationProgress progress = null)
		{
			Stopwatch stopwatch = new Stopwatch();
			float _num = 0f;
			foreach (GenPass pass in _passes)
			{
				_num += pass.Weight;
			}
			if (progress == null)
			{
				progress = new GenerationProgress();
			}
			progress.TotalWeight = _num;
			Main.menuMode = 888;
			Main.MenuUI.SetState(new UIWorldLoad(progress));

			_passesDict=new OrderedDictionary();
			foreach (GenPass pass3 in _passes)
			{
				if(!_passesDict.Contains(pass3.Name))
				{
					_passesDict.Add(pass3.Name,new List<GenPass>());
				}
				((List<GenPass>)_passesDict[pass3.Name]).Add(pass3);
			}

			runParallel( progress, stopwatch, new String[] {"Reset"});
			runParallel( progress, stopwatch, new String[] {"Terrain"});
			runParallel( progress, stopwatch, new String[] {"Tunnels","Sand","Mount Caves","Dirt Wall Backgrounds","Rocks In Dirt","Dirt In Rocks","Clay","Small Holes","Dirt Layer Caves","Rock Layer Caves","Surface Caves"});
			runParallel( progress, stopwatch, new String[] {"Slush Check"});
			runParallel( progress, stopwatch, new String[] {"Grass","Jungle"});
			runParallel( progress, stopwatch, new String[] {"Marble","Granite","Mud Caves To Grass","Full Desert","Floating Islands"});
			runParallel( progress, stopwatch, new String[] {"Mushroom Patches"});
			runParallel( progress, stopwatch, new String[] {"Mud To Dirt","Silt","Shinies","Webs","Underworld","Lakes","Dungeon","Corruption","Slush"});
			runParallel( progress, stopwatch, new String[] {"Mud Caves To Grass","Beaches","Gems","Gravitating Sand","Clean Up Dirt","Pyramids"});
			runParallel( progress, stopwatch, new String[] {"Dirt Rock Wall Runner","Living Trees"});
			runParallel( progress, stopwatch, new String[] {"Wood Tree Walls","Altars","Wet Jungle","Remove Water From Sand","Jungle Temple","Hives","Jungle Chests"});
			runParallel( progress, stopwatch, new String[] {"Smooth World","Settle Liquids"});
			runParallel( progress, stopwatch, new String[] {"Waterfalls","Ice","Wall Variety","Traps","Life Crystals","Statues","Buried Chests","Surface Chests","Jungle Chests Placement"});
			runParallel( progress, stopwatch, new String[] {"Water Chests","Spider Caves"});
			runParallel( progress, stopwatch, new String[] {"Gem Caves"});
			runParallel( progress, stopwatch, new String[] {"Moss"});
			runParallel( progress, stopwatch, new String[] {"Temple","Ice Walls","Jungle Trees","Floating Island Houses"});
			runParallel( progress, stopwatch, new String[] {"Quick Cleanup"});
			runParallel( progress, stopwatch, new String[] {"Pots","Hellforge","Spreading Grass","Piles"});
			runParallel( progress, stopwatch, new String[] {"Moss"});
			runParallel( progress, stopwatch, new String[] {"Spawn Point","Grass Wall","Guide","Sunflowers","Planting Trees","Herbs","Dye Plants"});
			runParallel( progress, stopwatch, new String[] {"Webs And Honey"});
			runParallel( progress, stopwatch, new String[] {"Weeds","Mud Caves To Grass","Jungle Plants","Vines","Flowers","Mushrooms","Stalac"});
			runParallel( progress, stopwatch, new String[] {"Gems In Ice Biome","Random Gems"});
			runParallel( progress, stopwatch, new String[] {"Moss Grass"});
			runParallel( progress, stopwatch, new String[] {"Muds Walls In Jungle","Larva"});
			runParallel( progress, stopwatch, new String[] {"Settle Liquids Again"});
			runParallel( progress, stopwatch, new String[] {"Tile Cleanup"});
			runParallel( progress, stopwatch, new String[] {"Lihzahrd Altars"});

			while(_passes.Count >= 1)
			{
				String[] genNameList=new String[1];
				genNameList[0] = _passes[0].Name;
				runParallel(progress,stopwatch,genNameList);
			};
		}

		private void runParallel(GenerationProgress progress,Stopwatch stopwatch,String[] genNameList)
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

			var partitioner = Partitioner.Create<GenPass>(genPassList, EnumerablePartitionerOptions.NoBuffering);
			Parallel.ForEach(partitioner,new ParallelOptions() {  MaxDegreeOfParallelism = 4},pass2 =>
			// Parallel.ForEach(partitioner,pass2 =>
			{
				gen(pass2,progress,stopwatch);
			});

		}

		private void gen(GenPass pass2,GenerationProgress progress,Stopwatch stopwatch)
		{
			WorldGen._genRand = new UnifiedRandom(_seed);
			Main.rand = new UnifiedRandom(_seed);
			stopwatch.Start();
			progress.Start(pass2.Weight);
			try
			{
				pass2.Apply(progress);
			}
			catch (Exception ex)
			{
				string text = string.Join("\n", Language.GetTextValue("tModLoader.WorldGenError"), pass2.Name, ex);
				// Logging.tML.Error((object)text);
				FieldInfo tMLInfo= Common.getFieldInfo(typeof(Logging),"tML");
				if(tMLInfo == null)
				{
					throw ex;
				}
				((ILog)tMLInfo.GetValue(tMLInfo)).Error((object)text);

				// Interface.errorMessage.Show(text, 0);
				throw ex;
			}
			progress.End();
			stopwatch.Reset();
		}
    }
}