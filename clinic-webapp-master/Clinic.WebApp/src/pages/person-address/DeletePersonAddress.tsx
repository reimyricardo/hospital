import React from "react";
import { useForm } from "react-hook-form";
import { Toaster, toast } from "sonner";
import { useHttpMethod } from "../../hooks/useHttpMethod";
import { DeletePersonAddressForm } from "../../models/input.model";
import Form from "../../shared/components/Form";
import InputErrorMessage from "../../shared/components/InputErrorMessage";
import { enviroments } from "../../shared/enviroments/enviroments.dev";
import { isHttpMethodHookError } from "../../shared/utils";

function DeletePersonAddress(): React.JSX.Element {
  const {
    handleSubmit,
    register,
    reset,
    formState: { errors },
  } = useForm<DeletePersonAddressForm>();

  const { postData } = useHttpMethod<DeletePersonAddressForm>({
    url: enviroments.deletePersonAddress,
  });

  const onSubmit = handleSubmit(async (data) => {
    const response = await postData({ body: data, method: "DELETE" });

    if (isHttpMethodHookError(response)) {
      if (response !== null) {
        toast.error(response.message);
      } else {
        toast.error("An error occurred while deleting the person address.");
      }
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
      <Form title="Delete Person Address Form" onSubmit={onSubmit}>
        <div className="mb-3">
          <label htmlFor="addressId" className="form-label">
            Address ID
          </label>
          <input
            className="form-control"
            id="addressId"
            type="number"
            {...register("addressId", {
              required: {
                value: true,
                message: "The address ID is required",
              },
            })}
          />
          {errors.addressId && (
            <InputErrorMessage message={errors.addressId.message} />
          )}
        </div>
      </Form>
    </>
  );
}

export default DeletePersonAddress;
