
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.IO;
using Terraria.GameContent.Generation;

namespace WorldGenSuperFast
{
	class World : ModWorld
	{
		FieldInfo _passesInfo=null;
		List<GenPass> _passes;

		WorldGenerator _generator;

		private List<Exception> currnetError;

		override public void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight){
			
			FieldInfo _generatorInfo = Common.getFieldInfo(typeof(WorldGen),"_generator");
			if(_generatorInfo == null)
			{
				return;
			}
			this._generator=(WorldGenerator)_generatorInfo.GetValue(_generatorInfo);
			
			this._passesInfo = Common.getFieldInfo(typeof(WorldGenerator),"_passes");
			if(this._passesInfo == null)
			{
				return;
			}
			this._passes=(List<GenPass>)_passesInfo.GetValue(_generator);

			FieldInfo _seedInfo=Common.getFieldInfo(typeof(WorldGenerator),"_seed");
			if(this._passesInfo == null)
			{
				return;
			}
			int _seed = (int)_seedInfo.GetValue(_generator);

			this._passesInfo.SetValue(this._generator,new List<GenPass>());
			WorldGeneratorRenew worldGeneratorRenew = new WorldGeneratorRenew(_seed);
			worldGeneratorRenew._passes=this._passes;
			currnetError = worldGeneratorRenew.GenerateWorld();

			if(currnetError.Count >= 1)
			{
				List<GenPass> genpassList = new List<GenPass>();
				
				foreach(Exception exception in currnetError)
				{
					genpassList.Add(new PassLegacy("viewError",delegate(GenerationProgress progress)
					{
						throw exception;
					}));
				}

				_passesInfo.SetValue(this._generator,genpassList);
			}

		}

		override public void PostWorldGen()
		{
			if(_passesInfo != null)
			{
				_passesInfo.SetValue(this._generator,this._passes);
			}
		}
    }
}