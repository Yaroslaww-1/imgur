import axios, { AxiosInstance, AxiosError } from "axios";

import { stringifyParams } from "@common/helpers/url.helper";
import { AuthService } from "./services/auth.service";

const BASE_URL = process.env.REACT_APP_API_URL || "/api";

class Api {
  private readonly instance: AxiosInstance;
  private readonly commonHeaders: {
    [key in string]: string;
  };
  private readonly formHeaders: {
    [key in string]: string;
  };
  constructor() {
    this.instance = axios.create({
      baseURL: BASE_URL,
      headers: {
        "Content-Type": "application/json",
      },
    });
    this.instance.interceptors.request.use(config => {
      config.headers.Authorization = `Bearer ${localStorage.getItem(
        "accessToken",
      )}`;
      return config;
    });
    this.instance.interceptors.response.use(
      config => {
        return config;
      },
      async error => {
        const originalRequest = error.config;
        if (
          error.response.status == 401 &&
          error.config &&
          !error.config._isRetry
        ) {
          originalRequest._isRetry = true;
          try {
            const responseTokens = await AuthService.refresh(
              localStorage.getItem("refreshToken") || "",
            );
            localStorage.setItem("accessToken", responseTokens.access_token);
            localStorage.setItem("refreshToken", responseTokens.refresh_token);
            return this.instance.request(originalRequest);
          } catch (e) {
            console.log("Not authorized");
          }
        }
        throw error;
      },
    );
    this.commonHeaders = { "Content-Type": "application/json" };
    this.formHeaders = { "Content-Type": "application/x-www-form-urlencoded" };
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

  async getAsForm<Response = unknown, Params = unknown>(
    url: string,
    params?: Params,
  ): Promise<Response> {
    const response = await this.instance
      .get<Response>(`${url}?${stringifyParams(params)}`, {
        headers: this.formHeaders,
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
      .post(url, payload, {
        headers: this.commonHeaders,
      })
      .then(({ data }) => data)
      .catch(this.handleError);
    return this.validateAndReturnResponse<Response>(response);
  }

  async postAsForm<Response = unknown, Payload = unknown>(
    url: string,
    payload: Payload,
  ): Promise<Response> {
    const response = await this.instance
      .post(url, stringifyParams(payload), {
        headers: this.formHeaders,
      })
      .then(({ data }) => data)
      .catch(this.handleError);
    return this.validateAndReturnResponse<Response>(response);
  }

  async put<Response = unknown, Payload = unknown>(
    url: string,
    payload: Payload,
  ): Promise<Response> {
    const response = await this.instance
      .put(url, payload, {
        headers: this.commonHeaders,
      })
      .then(({ data }) => data)
      .catch(this.handleError);
    return this.validateAndReturnResponse<Response>(response);
  }

  async delete<Response = unknown, Payload = unknown>(
    url: string,
    data?: Payload,
  ): Promise<Response> {
    const response = await this.instance
      .delete(url, {
        headers: this.commonHeaders,
        data,
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

export default new Api();
