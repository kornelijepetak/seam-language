namespace SeamCompiler.Utils;

public sealed class Either<TLeft, TRight>
{
    private readonly TLeft left;
    private readonly TRight right;

    public bool IsLeft { get; }

    public bool IsRight => !IsLeft;

    public TLeft Left => IsLeft
        ? left
        : throw new InvalidOperationException("Cannot use the Right property in an Either object created with a left value");

    public TRight Right => IsRight
        ? right
        : throw new InvalidOperationException("Cannot use the Left property in an Either object created with a right value");

    private Either(TLeft left)
    {
        this.left = left;
        right = default!;
        IsLeft = true;
    }

    private Either(TRight right)
    {
        this.right = right;
        left = default!;
        IsLeft = false;
    }

    public void Match(Action<TLeft> leftAction, Action<TRight> rightAction)
    {
        if (IsLeft)
            leftAction(left);
        else
            rightAction(right);
    }

    public T Select<T>(Func<TLeft, T> leftAction, Func<TRight, T> rightFunc) 
        => IsLeft ? leftAction(left) : rightFunc(right);

    public static Either<TLeft, TRight> WithLeft(TLeft left)
        => new(left);

    public static Either<TLeft, TRight> WithRight(TRight right)
        => new(right);

    public static implicit operator Either<TLeft, TRight>(TLeft value)
        => Either<TLeft, TRight>.WithLeft(value);

    public static implicit operator Either<TLeft, TRight>(TRight value)
        => Either<TLeft, TRight>.WithRight(value);
}
