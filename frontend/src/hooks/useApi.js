import axios from "axios";
import { apiBaseUrl } from "../common/constants";

const useApi = (token) => {
  const api = axios.create({
    baseURL: apiBaseUrl,
    timeout: 10000,
  });

  api.interceptors.response.use(
    (response) => response,
    (error) => {
      console.error("API error: ", error);
      return Promise.reject(error);
    }
  );

  if (token) {
    api.defaults.headers.common.Authorization = `Bearer ${token}`;
  }

  return api;
};

export default useApi;
