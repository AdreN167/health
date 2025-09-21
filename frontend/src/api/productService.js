import useApi from "../hooks/useApi";

export const getProducts = async (token) => {
  const api = useApi(token);
  const response = await api.get("/api/Product");
  return response.data.data;
};

export const createProduct = async (token, body) => {
  const api = useApi(token);
  const response = await api.post("/api/Product", body, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
  return response.data.data;
};

export const updateProduct = async (token, body) => {
  const api = useApi(token);
  const response = await api.put("/api/Product", body, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
  return response.data.data;
};

export const deleteProduct = async (token, id) => {
  const api = useApi(token);
  const response = await api.delete("/api/Product/" + id);
  return response.data.data;
};
