import { useForm } from "react-hook-form";
import { Toaster, toast } from "sonner";
import { useHttpMethod } from "../../hooks/useHttpMethod";
import { UpdatePatientForm } from "../../models/input.model";
import Form from "../../shared/components/Form";
import InputErrorMessage from "../../shared/components/InputErrorMessage";
import { enviroments } from "../../shared/enviroments/enviroments.dev";
import { isHttpMethodHookError } from "../../shared/utils";

function UpdatePatient(): React.JSX.Element {
  const {
    handleSubmit,
    formState: { errors },
    register,
    reset,
  } = useForm<UpdatePatientForm>();

  const { postData, error } = useHttpMethod({ url: enviroments.updatePatient });

  const onSubmit = handleSubmit(async (data) => {
    const response = await postData({ body: data, method: "PUT" });

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
      <Form title="Update Patient Form" onSubmit={onSubmit}>
        <div className="mb-3">
          <label htmlFor="name" className="form-label">
            Name
          </label>
          <input
            type="text"
            className="form-control"
            id="name"
            {...register("name", {
              required: {
                value: true,
                message: "The name is required",
              },
            })}
          />
          {errors.name && <InputErrorMessage message={errors.name.message} />}
        </div>
        <div className="mb-3">
          <label htmlFor="telephone" className="form-label">
            Telephone
          </label>
          <input
            type="text"
            className="form-control"
            id="telephone"
            {...register("telephone", {
              required: {
                value: true,
                message: "The telephone is required",
              },
              pattern: {
                value: /^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$/,
                message: "The telephone is invalid",
              },
            })}
          />
          {errors.telephone && (
            <InputErrorMessage message={errors.telephone.message} />
          )}
        </div>
        <div className="mb-3">
          <label htmlFor="nif" className="form-label">
            NIF
          </label>
          <input
            type="text"
            className="form-control"
            id="nif"
            {...register("nif", {
              required: {
                value: true,
                message: "The NIF is required",
              },
              pattern: {
                value: /^\d{9}$/,
                message: "The NIF is invalid",
              },
            })}
          />
          {errors.nif && <InputErrorMessage message={errors.nif.message} />}
        </div>
      </Form>
    </>
  );
}

export default UpdatePatient;
