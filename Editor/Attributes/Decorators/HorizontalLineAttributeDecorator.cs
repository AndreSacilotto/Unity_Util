using System;
using UnityEditor;
using UnityEngine;

namespace Spectra.Attributes
{
	[CustomPropertyDrawer(typeof(HorizontalLineAttribute), false)]
	public class HorizontalLineAttributeDecorator : DecoratorDrawer
	{
		public override float GetHeight()
		{
			var atr = attribute as HorizontalLineAttribute;
			return EditorGUIUtility.singleLineHeight + atr.lineHeight + atr.spaceHeight;
		}

		public override void OnGUI(Rect position)
		{
			var atr = attribute as HorizontalLineAttribute;

			Rect rect = EditorGUI.IndentedRect(position);

			rect.y += GetHeight() / 3.0f;
			rect.height = atr.lineHeight;
			EditorGUI.DrawRect(rect, atr.lineColor);
		}
	}
}
