import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useForm } from "react-hook-form";
import { Toaster, toast } from "sonner";
import { useFetch } from "../../hooks/useFetch";
import { useHttpMethod } from "../../hooks/useHttpMethod";
import { DoctorPosition } from "../../models/doctor.model";
import { CreateDoctorForm } from "../../models/input.model";
import { PagedList } from "../../models/pagedList.model";
import Form from "../../shared/components/Form";
import InputErrorMessage from "../../shared/components/InputErrorMessage";
import Loader from "../../shared/components/Loader";
import { enviroments } from "../../shared/enviroments/enviroments.dev";
import { isHttpMethodHookError } from "../../shared/utils/predicates";

function CreateDoctor(): React.JSX.Element {
  const {
    register,
    watch,
    handleSubmit,
    setValue,
    formState: { errors },
    reset,
  } = useForm<CreateDoctorForm>();

  const [position, isLoading, fetchError] = useFetch<
    PagedList<DoctorPosition[]>
  >({
    url: enviroments.getAllDoctorPosition,
    params: {
      page: "1",
      pageSize: "999",
    },
  });

  const { postData } = useHttpMethod<CreateDoctorForm>({
    url: enviroments.createDoctor,
  });

  const onSubmit = handleSubmit(async (data) => {
    const response = await postData({
      body: data,
      method: "POST",
    });

    if (isHttpMethodHookError(response) && response !== null) {
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
      {fetchError && (
        <>
          {(() => {
            toast.error(fetchError.message);
            return;
          })()}
        </>
      )}
      {isLoading && <Loader isLoading={isLoading} size={65} />}
      {!isLoading && (
        <Form title="Create Doctor Form" onSubmit={onSubmit}>
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
          <div className="mb-3">
            <label htmlFor="socialNumber" className="form-label">
              Social Number
            </label>
            <input
              type="number"
              className="form-control"
              id="socialNumber"
              {...register("socialNumber", {
                required: {
                  value: true,
                  message: "The Social Number is required",
                },
                pattern: {
                  value: /^\d{9}$/,
                  message: "The Social Number is invalid",
                },
              })}
            />
            {errors.socialNumber && (
              <InputErrorMessage message={errors.socialNumber.message} />
            )}
          </div>
          <div className="mb-3">
            <label htmlFor="collegueNumber" className="form-label">
              Collegue Number
            </label>
            <input
              type="number"
              className="form-control"
              id="collegueNumber"
              {...register("collegueNumber", {
                required: {
                  value: true,
                  message: "The Collegue Number is required",
                },
                pattern: {
                  value: /^\d{6}$/,
                  message: "The Collegue Number is invalid",
                },
              })}
            />
            {errors.collegueNumber && (
              <InputErrorMessage message={errors.collegueNumber.message} />
            )}
          </div>
          <div className="mb-3">
            <label
              htmlFor="datepicker"
              style={{ display: "block", marginBottom: 10 }}
            >
              Start Date
            </label>
            <DatePicker
              id="datepicker"
              selected={watch("startDate")}
              onChange={(date) => setValue("startDate", date)}
              className="form-control"
              dateFormat="yyyy-MM-dd"
              isClearable
            />
          </div>
          <div className="mb-3">
            <label htmlFor="doctorPosition" className="form-label">
              Doctor Position
            </label>
            <select
              id="doctorPosition"
              {...register("doctorPosition", {
                required: {
                  value: true,
                  message: "The doctor position is required",
                },
              })}
              className="form-select"
            >
              {position?.data.items.map(({ positionName }, index) => {
                return <option key={index}>{positionName}</option>;
              })}
            </select>
            {errors.doctorPosition && (
              <InputErrorMessage message={errors.doctorPosition.message} />
            )}
          </div>
        </Form>
      )}
    </>
  );
}

export default CreateDoctor;
