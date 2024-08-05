import React from "react";
import { useForm } from "react-hook-form";
import { Toaster, toast } from "sonner";
import { useHttpMethod } from "../../hooks/useHttpMethod";
import { CreateEmployeePositionForm } from "../../models/input.model";
import Form from "../../shared/components/Form";
import InputErrorMessage from "../../shared/components/InputErrorMessage";
import { enviroments } from "../../shared/enviroments/enviroments.dev";
import { isHttpMethodHookError } from "../../shared/utils";

function CreateEmployeePosition(): React.JSX.Element {
  const {
    handleSubmit,
    register,
    reset,
    formState: { errors },
  } = useForm<CreateEmployeePositionForm>();

  const { postData } = useHttpMethod<CreateEmployeePositionForm>({
    url: enviroments.createEmployeePosition,
  });

  const onSubmit = handleSubmit(async (data) => {
    const response = await postData({ body: data, method: "POST" });

    if (isHttpMethodHookError(response)) {
      toast.error(response.message);
      return;
    } else {
      toast.success("Success");
      reset();
      return;
    }
  });

  return (
    <>
      <Toaster position="bottom-center" />
      <Form title="Create Employee Position Form" onSubmit={onSubmit}>
        <div className="mb-3">
          <label htmlFor="positionName" className="form-label">
            Position Name
          </label>
          <input
            className="form-control"
            id="positionName"
            type="text"
            {...register("positionName", {
              required: {
                value: true,
                message: "The position name is required",
              },
            })}
          />
          {errors.positionName && (
            <InputErrorMessage message={errors.positionName.message} />
          )}
        </div>
      </Form>
    </>
  );
}

export default CreateEmployeePosition;
