﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace BayatGames.Serialization.Formatters.Binary
{

	/// <summary>
	/// Binary formatter.
	/// Serialize and Deserialize binary representations.
	/// </summary>
	public class BinaryFormatter
	{

		#region Fields

		/// <summary>
		/// The surrogate selector.
		/// </summary>
		protected ISurrogateSelector m_SurrogateSelector;

		/// <summary>
		/// The context.
		/// </summary>
		protected StreamingContext m_Context;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the surrogate selector.
		/// </summary>
		/// <value>The surrogate selector.</value>
		public virtual ISurrogateSelector surrogateSelector
		{
			get
			{
				return m_SurrogateSelector;
			}
			set
			{
				m_SurrogateSelector = value;
			}
		}

		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		/// <value>The context.</value>
		public virtual StreamingContext context
		{
			get
			{
				return m_Context;
			}
			set
			{
				m_Context = value;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="BayatGames.Serialization.Formatters.Json.JsonFormatter"/> class.
		/// </summary>
		public BinaryFormatter () : this ( null, new StreamingContext ( StreamingContextStates.All ) )
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BayatGames.Serialization.Formatters.Json.JsonFormatter"/> class.
		/// </summary>
		/// <param name="selector">Selector.</param>
		/// <param name="context">Context.</param>
		public BinaryFormatter ( ISurrogateSelector selector, StreamingContext context )
		{
			m_SurrogateSelector = selector;
			m_Context = context;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Serialize the specified value.
		/// </summary>
		/// <param name="value">Value.</param>
		public byte[] Serialize ( object value )
		{
			using ( MemoryStream output = new MemoryStream () )
			{
				Serialize ( output );
				return output.ToArray ();
			}
		}

		/// <summary>
		/// Serialize the specified output and value.
		/// </summary>
		/// <param name="output">Output.</param>
		/// <param name="value">Value.</param>
		public void Serialize ( Stream output, object value )
		{
			using ( BinaryObjectWriter writer = new BinaryObjectWriter ( output, m_SurrogateSelector, m_Context ) )
			{
				writer.Write ( value );
			}
		}

		/// <summary>
		/// Deserialize the specified buffer and type.
		/// </summary>
		/// <param name="buffer">Buffer.</param>
		/// <param name="type">Type.</param>
		public object Deserialize ( byte [] buffer, Type type )
		{
			using ( MemoryStream input = new MemoryStream ( buffer ) )
			{
				return Deserialize ( input, type );
			}
		}

		/// <summary>
		/// Deserialize the specified input.
		/// </summary>
		/// <param name="input">Input.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public object Deserialize ( Stream input, Type type )
		{
			using ( BinaryObjectReader reader = new BinaryObjectReader ( input, m_SurrogateSelector, m_Context ) )
			{
				return reader.Read ( type );
			}
		}

		public static byte[] Serialize ( object value )
		{
			BinaryFormatter formatter = new BinaryFormatter ();
			return formatter.Serialize ( value );
		}

		public static void Serialize ( Stream output, object value )
		{
			BinaryFormatter formatter = new BinaryFormatter ();
			formatter.Serialize ( output, value );
		}

		public static object Deserialize ( byte [] buffer, object value )
		{
			BinaryFormatter formatter = new BinaryFormatter ();
		}

		#endregion
		
	}

}