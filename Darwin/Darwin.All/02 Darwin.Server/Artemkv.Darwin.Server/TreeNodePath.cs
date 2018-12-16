using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemkv.Darwin.Server
{
	/// <summary>
	/// Identifies the node path in the tree.
	/// </summary>
	public class TreeNodePath : IEnumerable<string>
	{
		#region Constants

		private const char Separator = '~';
		private const string RootPath = "!";

		#endregion Constants

		#region Class Members

		private string _path;
		private string _leaf;
		private int _length;

		#endregion Class Members

		#region Static Methods

		/// <summary>
		/// Returns the root path.
		/// </summary>
		public static TreeNodePath Root
		{
			get
			{
				return new TreeNodePath(RootPath);
			}
		}

		/// <summary>
		/// Tries to parse incoming string as a path.
		/// </summary>
		/// <param name="pathString">String with the path.</param>
		/// <param name="path">The path.</param>
		/// <returns>True, if parsed successfully; otherwise, False.</returns>
		public static bool TryParse(string pathString, out TreeNodePath path)
		{
			path = null;

			if (String.IsNullOrWhiteSpace(pathString))
			{
				return false;
			}

			string[] pathElements = pathString.Split(new char[1] { Separator });
			if (pathElements[0] != TreeNodePath.Root)
			{
				return false;
			}

			path = TreeNodePath.Root;
			for (int i = 1; i < pathElements.Length; i++)
			{
				path = path.Then(pathElements[i]);
			}

			return true;
		}

		#endregion Static Methods

		#region .Ctors

		private TreeNodePath(string basePath)
		{
			_path = basePath;
			_leaf = String.Empty;
			_length = 1;
		}

		private TreeNodePath(string basePath, string nextElement, int basePathLength)
		{
			_path = basePath + Separator + nextElement;
			_leaf = nextElement;
			_length = basePathLength + 1;
		}

		#endregion .Ctors

		#region Operators

		public static implicit operator String(TreeNodePath path)
		{
			return path.ToString();
		}

		public static bool operator ==(TreeNodePath a, TreeNodePath b)
		{
			// If both are null, or both are same instance, return true.
			if (System.Object.ReferenceEquals(a, b))
			{
				return true;
			}

			// If one is null, but not both, return false.
			if (((object)a == null) || ((object)b == null))
			{
				return false;
			}

			// Return true if the fields match:
			return a._path.Equals(b._path, StringComparison.InvariantCulture);
		}

		public static bool operator !=(TreeNodePath a, TreeNodePath b)
		{
			return !(a == b);
		}

		#endregion Operators

		#region Public Properties

		public string Leaf
		{
			get
			{
				return _leaf;
			}
		}

		public int Length
		{
			get
			{
				return _length;
			}
		}

		#endregion Public Properties

		#region Public Methods

		/// <summary>
		/// Returns the new path as combination of the current path and a next element.
		/// </summary>
		/// <param name="nextElement">The next element.</param>
		/// <returns>The new path.</returns>
		public TreeNodePath Then(string nextElement)
		{
			return new TreeNodePath(_path, nextElement, _length);
		}

		public override bool Equals(object obj)
		{
			var another = obj as TreeNodePath;
			if (another == null)
			{
				return false;
			}

			return this == another;
		}

		public override int GetHashCode()
		{
			return _path.GetHashCode();
		}

		public override string ToString()
		{
			return _path;
		}

		public IEnumerator<string> GetEnumerator()
		{
			string[] pathElements = _path.Split(new char[1] { Separator });
			foreach (var element in pathElements)
			{
				yield return element;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion Public Methods
	}
}
