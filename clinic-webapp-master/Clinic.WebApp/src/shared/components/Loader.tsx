import { BounceLoader } from "react-spinners";
import { LoaderProps } from "../../models/props/components.props";

function Loader({ isLoading, color, size }: LoaderProps): React.JSX.Element {
  return (
    <>
      <div
        className="container-fluid d-flex justify-content-center align-items-center"
        style={{ minHeight: "90vh" }}
      >
        <BounceLoader
          loading={isLoading}
          color={color ?? "#36d7b7"}
          size={size ?? 35}
        />
      </div>
    </>
  );
}

export default Loader;
