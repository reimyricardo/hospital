import React from "react";
import { useForm } from "react-hook-form";
import { Toaster, toast } from "sonner";
import { useHttpMethod } from "../../hooks/useHttpMethod";
import { CreatePersonAddressForm } from "../../models/input.model";
import Form from "../../shared/components/Form";
import InputErrorMessage from "../../shared/components/InputErrorMessage";
import { enviroments } from "../../shared/enviroments/enviroments.dev";
import { isHttpMethodHookError } from "../../shared/utils";

function CreatePersonAddress(): React.JSX.Element {
  const {
    handleSubmit,
    register,
    reset,
    formState: { errors },
  } = useForm<CreatePersonAddressForm>();

  const { postData } = useHttpMethod<CreatePersonAddressForm>({
    url: enviroments.createPersonAddress,
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
      <Form title="Create Person Address Form" onSubmit={onSubmit}>
        <div className="mb-3">
          <label htmlFor="personName" className="form-label">
            Person Name
          </label>
          <input
            className="form-control"
            id="personName"
            type="text"
            {...register("personName", {
              required: {
                value: true,
                message: "The person name is required",
              },
            })}
          />
          {errors.personName && (
            <InputErrorMessage message={errors.personName.message} />
          )}
        </div>
        <div className="mb-3">
          <label htmlFor="personNif" className="form-label">
            Person NIF
          </label>
          <input
            className="form-control"
            id="personNif"
            type="text"
            {...register("personNif", {
              required: {
                value: true,
                message: "The person NIF is required",
              },
            })}
          />
          {errors.personNif && (
            <InputErrorMessage message={errors.personNif.message} />
          )}
        </div>
        <div className="mb-3">
          <label htmlFor="streetNumber" className="form-label">
            Street Number
          </label>
          <input
            className="form-control"
            id="streetNumber"
            type="number"
            {...register("streetNumber", {
              required: {
                value: true,
                message: "The street number is required",
              },
            })}
          />
          {errors.streetNumber && (
            <InputErrorMessage message={errors.streetNumber.message} />
          )}
        </div>
        <div className="mb-3">
          <label htmlFor="addressLine1" className="form-label">
            Address Line 1
          </label>
          <input
            className="form-control"
            id="addressLine1"
            type="text"
            {...register("addressLine1", {
              required: {
                value: true,
                message: "The address line 1 is required",
              },
            })}
          />
          {errors.addressLine1 && (
            <InputErrorMessage message={errors.addressLine1.message} />
          )}
        </div>
        <div className="mb-3">
          <label htmlFor="addressLine2" className="form-label">
            Address Line 2
          </label>
          <input
            className="form-control"
            id="addressLine2"
            type="text"
            {...register("addressLine2")}
          />
        </div>
        <div className="mb-3">
          <label htmlFor="city" className="form-label">
            City
          </label>
          <input
            className="form-control"
            id="city"
            type="text"
            {...register("city", {
              required: {
                value: true,
                message: "The city is required",
              },
            })}
          />
          {errors.city && <InputErrorMessage message={errors.city.message} />}
        </div>
        <div className="mb-3">
          <label htmlFor="population" className="form-label">
            Population
          </label>
          <input
            className="form-control"
            id="population"
            type="number"
            {...register("population", {
              required: {
                value: true,
                message: "The population is required",
              },
            })}
          />
          {errors.population && (
            <InputErrorMessage message={errors.population.message} />
          )}
        </div>
        <div className="mb-3">
          <label htmlFor="postalCode" className="form-label">
            Postal Code
          </label>
          <input
            className="form-control"
            id="postalCode"
            type="number"
            {...register("postalCode", {
              required: {
                value: true,
                message: "The postal code is required",
              },
            })}
          />
          {errors.postalCode && (
            <InputErrorMessage message={errors.postalCode.message} />
          )}
        </div>
        <div className="mb-3">
          <label htmlFor="province" className="form-label">
            Province
          </label>
          <input
            className="form-control"
            id="province"
            type="text"
            {...register("province", {
              required: {
                value: true,
                message: "The province is required",
              },
            })}
          />
          {errors.province && (
            <InputErrorMessage message={errors.province.message} />
          )}
        </div>
      </Form>
    </>
  );
}

export default CreatePersonAddress;
