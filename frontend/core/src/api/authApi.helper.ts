import axios, { AxiosInstance, AxiosError } from "axios";

import { stringifyParams } from "@common/helpers/url.helper";

const BASE_URL = process.env.REACT_APP_API_URL || "/api";

class AuthApi {
  private readonly instance: AxiosInstance;
  private readonly commonHeaders: {
    [key in string]: string;
  };
  constructor() {
    this.instance = axios.create({
      baseURL: BASE_URL,
      headers: {
        "Content-Type": "application/x-www-form-urlencoded",
      },
    });
    this.commonHeaders = {
      "Content-Type": "application/x-www-form-urlencoded",
    };
  }

  async get<Response = unknown, Params = unknown>(
    url: string,
    params?: Params,
  ): Promise<Response> {
    const response = await this.instance
      .get<Response>(`${url}?${stringifyParams(params)}`, {
        headers: this.commonHeaders,
        data: {},
      })
      .then(({ data }) => data)
      .catch(this.handleError);
    return this.validateAndReturnResponse<Response>(response);
  }

  async post<Response = unknown, Payload = unknown>(
    url: string,
    payload: Payload,
  ): Promise<Response> {
    const response = await this.instance
      .post(url, stringifyParams(payload), {
        headers: this.commonHeaders,
      })
      .then(({ data }) => data)
      .catch(this.handleError);
    return this.validateAndReturnResponse<Response>(response);
  }

  private validateAndReturnResponse<Response>(
    responseData: Response | void,
  ): Response {
    if (!responseData) {
      throw new Error();
    } else {
      return responseData;
    }
  }

  private handleError(error: AxiosError) {
    if (error.response) {
      throw new Error(error.response.data.error);
    } else if (error.request) {
      throw new Error(error.request.responseText);
    } else {
      throw new Error(error.message);
    }
  }
}

export default new AuthApi();
