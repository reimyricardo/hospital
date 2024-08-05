import React from "react";
import { FormProps } from "../../models/props/form.props";

function Form({ children, title, onSubmit }: FormProps): React.JSX.Element {
  return (
    <>
      <div className="container d-flex flex-column justify-content-center align-items-center">
        <h1 className="display-2 mb-4">{title}</h1>
        <div className="col-lg-7">
          <form onSubmit={onSubmit}>
            {children}
            <div className=" mt-4 d-grid gap-2">
              <button type="submit" className="btn btn-outline-dark btn-lg">
                Submit
              </button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
}

export default Form;
