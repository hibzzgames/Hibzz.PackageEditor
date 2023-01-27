using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hibzz.PackageEditor
{
	public static class StringExtension
	{
		public static string Bold   (this string text)               => $"<b>{text}</b>";
		public static string Color  (this string text, string color) => $"<color={color}>{text}</color>";
		public static string Italic (this string text)               => $"<i>{text}</i>";
		public static string Size   (this string text, int size)     => $"<size={size}>{text}</size>";

	}
}
