import { useState } from "react";
import {
  PostHookDefaultResponse,
  PostHookError,
  PostHookOptions,
} from "../models/hooks/httpMethodHook.model";
import { ValidHttpMethods } from "../models/utils/http-method.model";
import Guard from "../shared/utils/guard";
import { isProblemDetails } from "../shared/utils/predicates";

function useHttpMethod<TRequest extends object>({
  url,
  headers,
}: PostHookOptions<TRequest>) {
  const [error, setError] = useState<Error | null>(null);

  const postData = async ({
    body,
    method,
  }: {
    body: TRequest;
    method: ValidHttpMethods;
  }): Promise<PostHookError | PostHookDefaultResponse | null> => {
    try {
      Guard.Against.EmptyString(url, "Invalid URL,check and try again");

      Guard.Against.EmptyObject(
        body,
        "Invalid body object, check and try again"
      );

      Guard.Against.EmptyObjectEntry(
        body,
        "Invalid body object entries, check and try again"
      );

      const response = await fetch(url, {
        method: method,
        body: JSON.stringify(body),
        headers: new Headers(
          headers ?? {
            "Content-Type": "application/json; charset=utf-8",
          }
        ),
      });

      if (response.ok) {
        return {
          status: response.status,
          url: response.url,
          ok: response.ok,
        };
      }
      const result = await response.json();

      if (!isValidStatusCode(response.status) && isProblemDetails(result)) {
        // eslint-disable-next-line @typescript-eslint/no-unused-vars
        const { errorType, ...currentError } = result.error;

        if (
          result.validationErrors !== undefined &&
          result.validationErrors.length > 0
        ) {
          const errors = result.validationErrors.map((error) => {
            // eslint-disable-next-line @typescript-eslint/no-unused-vars
            const { errorType, ...currentValidationError } = error;

            return currentValidationError;
          });

          errors.push(currentError);

          return {
            status: result.status,
            code: getStatusCodeDescription(response.status),
            message: result.detail,
            url: response.url,
            errors: errors,
            ok: response.ok,
          };
        }

        return {
          status: result.status,
          code: getStatusCodeDescription(response.status),
          message: result.detail,
          url: response.url,
          errors: [currentError],
          ok: response.ok,
        };
      }
    } catch (err: unknown) {
      if (err instanceof Error) {
        setError(err);
      }
    }

    return null;
  };

  return { error, postData };
}

function getStatusCodeDescription(status: number): string {
  switch (status) {
    case 400:
      return "ERR_BAD_REQUEST";
    case 404:
      return "ERR_NOT_FOUND";
    case 409:
      return "ERR_CONFLIT";
    case 500:
      return "ERR_INTERNAL_SERVER_ERROR";
    default:
      return "ERR_UNKNONW_SERVER_ERROR";
  }
}

function isValidStatusCode(status: number): boolean {
  return [200, 204].includes(status);
}

export { useHttpMethod };
