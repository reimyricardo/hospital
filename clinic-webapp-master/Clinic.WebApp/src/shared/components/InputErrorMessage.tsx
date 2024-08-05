function InputErrorMessage({
  message,
}: {
  message: string | undefined;
}): React.JSX.Element {
  return (
    <>
      <span className="text-danger">{message}</span>
    </>
  );
}

export default InputErrorMessage;
