class Guard {
  public static Against = new Guard();

  public EmptyString(input: string, message?: string): string {
    if (input.length === 0 || input === "") {
      throw new Error("The required input was empty" ?? message);
    }

    return input;
  }

  public EmptyObject(input: object, message?: string): object {
    if (Object.entries(input).length === 0) {
      throw new Error("The required input was empty" ?? message);
    }

    return input;
  }

  public EmptyObjectEntry(input: object, message?: string): object {
    const entries = Object.entries(input).reduce(
      (acc: unknown[], value: unknown[]) => [...acc, ...value],
      []
    );

    for (const entry of entries) {
      if (typeof entry === "string" && entry.length > 0) continue;

      if (entry instanceof Date && entry.toString().length > 0) continue;

      throw new Error("The required object entry are empty" ?? message);
    }

    return input;
  }
}

export default Guard;
