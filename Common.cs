
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

namespace WorldGenSuperFast
{
	class Common
	{
		public static FieldInfo getFieldInfo(Type type,String fieldName)
		{
			FieldInfo[] fields = type.GetFields(
				BindingFlags.NonPublic | 
				BindingFlags.Instance | 
				BindingFlags.GetField | 
				BindingFlags.FlattenHierarchy | 
				BindingFlags.SetField|
				BindingFlags.Static
			);

			if(fields != null)
			{
				foreach(FieldInfo field in fields)
				{
					if(fieldName.Equals(field.Name))
					{
						return field;
					}
				}
			}
			return null;
		}

        // public static Type GetType(string nameSpace,string className)
        // {
        //     Assembly assembly = Assembly.GetExecutingAssembly();
        //     return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal) && String.Equals(t.Name, className, StringComparison.Ordinal)).ToArray()[0];
        // }
    }
}