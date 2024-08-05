import { PostHookError } from "../../models/hooks/httpMethodHook.model";
import { ProblemDetails } from "../../models/problem-details.model";

export const isProblemDetails = (
  response: unknown
): response is ProblemDetails => {
  return (response as ProblemDetails).status !== undefined;
};

export const isHttpMethodHookError = (
  response: unknown
): response is PostHookError => {
  return (response as PostHookError).message !== undefined;
};
