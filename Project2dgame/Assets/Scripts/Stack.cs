using System;

/// <summary>
/// A custom Stack implementation.
/// </summary>
public class Stack<T>
{
	T[] buffer;

	int index = -1;

	/// <summary>
	/// Initializes a new instance of the <see cref="Stack`1"/> class.
	/// </summary>
	/// <param name="capacity">The capacity of this stack.</param>
	public Stack ( int capacity )
	{
		buffer = new T[capacity];
	}

	/// <summary>
	/// Push the specified value to the stack.
	/// </summary>
	/// <param name="value">Value.</param>
	public void Push( T value )
	{
		buffer[++index] = value;
	}

	/// <summary>
	/// Returns the last item from the stack.
	///   or the default value of the type stored in the stack if the stack is empty.
	/// </summary>
	public T Pop()
	{
		if ( index >= 0 )
			return buffer[index--];
		else
			return default(T);
	}

	/// <summary>
	/// Determines whether the stack is empty.
	/// </summary>
	/// <returns><c>true</c> if the stack is empty; otherwise, <c>false</c>.</returns>
	public bool IsEmpty()
	{
		return index == -1;
	}
}