import { useForm } from "react-hook-form";
import { Toaster, toast } from "sonner";
import { useHttpMethod } from "../../hooks/useHttpMethod";
import { CreateDoctorPositionForm } from "../../models/input.model";
import Form from "../../shared/components/Form";
import InputErrorMessage from "../../shared/components/InputErrorMessage";
import { enviroments } from "../../shared/enviroments/enviroments.dev";
import { isHttpMethodHookError } from "../../shared/utils";

function CreateDoctorPosition(): React.JSX.Element {
  const {
    handleSubmit,
    formState: { errors },
    register,
    reset,
  } = useForm<CreateDoctorPositionForm>();

  const { postData, error } = useHttpMethod<CreateDoctorPositionForm>({
    url: enviroments.createDoctorPosition,
  });

  const onSubmit = handleSubmit(async (data) => {
    const response = await postData({ body: data, method: "POST" });

    if (error !== null) {
      toast.error(error.message);
      return;
    }

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
      <Toaster />
      <Form title="Create Doctor Position Form" onSubmit={onSubmit}>
        <div className="mb-3">
          <label htmlFor="positionName" className="form-label">
            Position Name
          </label>
          <input
            type="text"
            className="form-control"
            id="positionName"
            {...register("positionName", {
              required: {
                value: true,
                message: "The name is required",
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

export default CreateDoctorPosition;
