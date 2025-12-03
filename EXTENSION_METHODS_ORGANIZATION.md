# Extension Methods Organization

The extension methods have been reorganized from a single monolithic `EnumerableExtensions.cs` file into focused, cohesive classes organized in the `Extensions` folder.

## New Structure

### 📂 Extensions Folder (`src/AdventOfCode.Solutions/Extensions/`)

#### 1. **ArrayExtensions.cs**
- **Purpose**: Array-specific operations
- **Methods**:
  - `Permutations()` - Generates all permutations of an array

#### 2. **StringExtensions.cs**
- **Purpose**: String manipulation and parsing
- **Methods**:
  - `ToLines()` - Splits string into lines
  - `ToLines<T>(Func<string, T>)` - Parses each line using selector
  - `ToLines<TResult>(Func<string, int, TResult>)` - Parses lines with index

#### 3. **ChunkingExtensions.cs**
- **Purpose**: Breaking sequences into chunks
- **Methods**:
  - `Chunk(int)` - Splits sequence into fixed-size chunks
  - `ChunkBy(Func<T, bool>)` - Splits sequence using predicate
  - `ToSequences(Func<T, bool>)` - Groups elements into sequences

#### 4. **WindowingExtensions.cs**
- **Purpose**: Sliding window and pairwise operations
- **Methods**:
  - `Window(int, bool)` - Non-overlapping windows
  - `SlidingWindow(int)` - Overlapping sliding windows
  - `Pairwise()` - Creates pairs of consecutive elements

#### 5. **AggregationExtensions.cs**
- **Purpose**: Aggregation and reduction operations
- **Methods**:
  - `MinMax()` - Finds both min and max in one pass
  - `MinMax(IComparer<T>)` - With custom comparer
  - `MinMaxBy<TValue>(Func<T, TValue>)` - Projects then finds min/max

#### 6. **CombinatorialExtensions.cs**
- **Purpose**: Combinatorial operations
- **Methods**:
  - `Combinations(int)` - Generates all combinations of specified width

#### 7. **SequenceExtensions.cs**
- **Purpose**: General sequence operations
- **Methods**:
  - `Flatten(Func<T, IEnumerable<T>>)` - Recursive flattening
  - `TakeUntil(Func<T, bool>)` - Takes elements until predicate is true
  - `Pivot()` - Transposes 2D sequences
  - `ToEnumerable<T>()` - Converts single item to enumerable

## Benefits of This Organization

### ✅ Improved Discoverability
Each extension class has a clear, focused purpose making it easier to find the right method.

### ✅ Better Maintainability
Related methods are grouped together, making it easier to:
- Add new methods in the right place
- Find and fix bugs
- Understand dependencies

### ✅ Cleaner Code Structure
- Smaller files (20-60 lines each vs 270+ lines)
- Single Responsibility Principle
- Easier to review and test

### ✅ Logical Grouping
Methods are organized by:
- **Data type** (Array, String)
- **Operation type** (Chunking, Windowing, Aggregation)
- **Purpose** (Combinatorial, Sequence manipulation)

## Migration Notes

- All extension methods remain in the `AdventOfCode.Solutions` namespace
- No changes required in consuming code
- All existing functionality preserved
- Build and tests pass successfully

## File Sizes

| File | Lines | Purpose |
|------|-------|---------|
| `ArrayExtensions.cs` | 30 | Array permutations |
| `StringExtensions.cs` | 18 | String parsing |
| `ChunkingExtensions.cs` | 57 | Sequence chunking |
| `WindowingExtensions.cs` | 60 | Windowing operations |
| `AggregationExtensions.cs` | 64 | Min/Max operations |
| `CombinatorialExtensions.cs` | 39 | Combinations |
| `SequenceExtensions.cs` | 46 | General sequences |
| **Total** | **314** | *vs 277 in original* |

*Note: Slight increase due to better formatting and organization*

