import useApi from "../hooks/useApi";

export const getTrainers = async (token) => {
  const api = useApi(token);
  const response = await api.get("/api/Trainer");
  return response.data.data;
};

export const createTrainer = async (token, body) => {
  const api = useApi(token);
  const response = await api.post("/api/Trainer", body, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
  return response.data.data;
};

export const updateTrainer = async (token, body) => {
  const api = useApi(token);
  const response = await api.put("/api/Trainer", body, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
  return response.data.data;
};

export const deleteTrainer = async (token, id) => {
  const api = useApi(token);
  const response = await api.delete("/api/Trainer/" + id);
  return response.data.data;
};
