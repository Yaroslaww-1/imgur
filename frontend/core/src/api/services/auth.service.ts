import api from "../api.helper";
import authApi from "../authApi.helper";

import { IUser } from "@models/user.model";

const tokenEndpoint = "/api/users/connect/token";
const createUserEndpoint = "/api/users/users";
const authUserEndpoint = "/api/users/authenticatedUser";

interface IAuthResponse {
  access_token: string;
  expires_in: number;
  refresh_token: string;
  scope: string;
  token_type: string;
}

interface IRefreshResponse {
  id_token: string;
  access_token: string;
  expires_in: number;
  token_type: string;
  refresh_token: string;
  scope: string;
}

export class AuthService {
  static async login(
    username: string,
    password: string,
  ): Promise<IAuthResponse> {
    return authApi.post(tokenEndpoint, {
      grant_type: "password",
      client_id: "ro.client",
      client_secret: "secret",
      username,
      password,
    });
  }

  static async registration(
    email: string,
    name: string,
    password: string,
  ): Promise<IAuthResponse> {
    return authApi.postJson(createUserEndpoint, {
      email,
      name,
      password,
      roles: ["User"],
    });
  }

  static async getAuthenticatedUser(
    username?: string,
    password?: string,
  ): Promise<IUser> {
    const user = api.getAsForm<IUser>(authUserEndpoint, {
      grant_type: "password",
      client_id: "ro.client",
      client_secret: "secret",
      username,
      password,
    });
    return user;
  }

  static async refresh(refresh_token: string): Promise<IRefreshResponse> {
    return authApi.post(tokenEndpoint, {
      grant_type: "refresh_token",
      client_id: "ro.client",
      client_secret: "secret",
      refresh_token,
    });
  }
}
