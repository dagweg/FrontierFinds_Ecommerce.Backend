namespace Ecommerce.Application.Common.Utilities;

using System;
using System.Linq;

public static class StringAlgorithms
{
  /// <summary>
  /// Calculates the Levenshtein distance between two strings.
  /// The Levenshtein distance is the minimum number of single-character edits
  /// (insertions, deletions or substitutions) required to change one word into the other.
  /// </summary>
  /// <param name="str1">The first string.</param>
  /// <param name="str2">The second string.</param>
  /// <returns>The Levenshtein distance between the two strings.</returns>
  public static int LevenshteinDistance(string str1, string str2)
  {
    if (str1 == null || str2 == null)
    {
      throw new ArgumentNullException("Strings cannot be null.");
    }

    int n = str1.Length;
    int m = str2.Length;

    // Create a distance matrix (d) of size (n+1) x (m+1)
    int[,] d = new int[n + 1, m + 1];

    // Initialize the first row and first column
    // d[i, 0] represents the distance of transforming the first i characters of str1 to an empty string (i deletions)
    // d[0, j] represents the distance of transforming an empty string to the first j characters of str2 (j insertions)
    for (int i = 0; i <= n; i++)
    {
      d[i, 0] = i;
    }
    for (int j = 0; j <= m; j++)
    {
      d[0, j] = j;
    }

    // Iterate through the matrix to fill in the distances
    for (int j = 1; j <= m; j++)
    {
      for (int i = 1; i <= n; i++)
      {
        int cost = (str1[i - 1] == str2[j - 1]) ? 0 : 1; // Cost is 0 if characters are the same, 1 if different

        // d[i, j] is the minimum of:
        // 1. d[i-1, j] + 1: Deletion (delete a character from str1 to match str2)
        // 2. d[i, j-1] + 1: Insertion (insert a character into str1 to match str2)
        // 3. d[i-1, j-1] + cost: Substitution (substitute a character in str1 to match str2, or no operation if characters are the same)
        d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
      }
    }

    // The Levenshtein distance is the value in the bottom-right corner of the matrix
    return d[n, m];
  }
}
