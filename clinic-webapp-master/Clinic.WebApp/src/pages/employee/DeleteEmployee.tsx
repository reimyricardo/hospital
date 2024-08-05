import React from "react";
import { useForm } from "react-hook-form";
import { Toaster, toast } from "sonner";
import { useHttpMethod } from "../../hooks/useHttpMethod";
import { DeleteEmployeeForm } from "../../models/input.model";
import Form from "../../shared/components/Form";
import InputErrorMessage from "../../shared/components/InputErrorMessage";
import { enviroments } from "../../shared/enviroments/enviroments.dev";
import { isHttpMethodHookError } from "../../shared/utils";

function DeleteEmployee(): React.JSX.Element {
  const {
    handleSubmit,
    register,
    reset,
    formState: { errors },
  } = useForm<DeleteEmployeeForm>();

  const { postData } = useHttpMethod<DeleteEmployeeForm>({
    url: enviroments.deleteEmployee,
  });

  const onSubmit = handleSubmit(async (data) => {
    const response = await postData({ body: data, method: "DELETE" });

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
      <Form title="Delete Employee Form" onSubmit={onSubmit}>
        <div className="mb-3">
          <label htmlFor="employeeName" className="form-label">
            Employee Name
          </label>
          <input
            className="form-control"
            id="employeeName"
            type="string"
            {...register("employeeName", {
              required: {
                value: true,
                message: "The employee Name is required",
              },
            })}
          />
          {errors.employeeName && (
            <InputErrorMessage message={errors.employeeName.message} />
          )}
        </div>
        <div className="mb-3">
          <label htmlFor="employeeNif" className="form-label">
            Employee Nif
          </label>
          <input
            type="text"
            {...register("employeeNif", {
              required: {
                value: true,
                message: "The employee nif is required",
              },
              pattern: {
                value: /^\d{9}$/,
                message: "The employee nif is invalid",
              },
            })}
            className="form-control"
            id="employeeNif"
          />
          {errors.employeeNif && (
            <InputErrorMessage message={errors.employeeNif.message} />
          )}
        </div>
      </Form>
    </>
  );
}

export default DeleteEmployee;
