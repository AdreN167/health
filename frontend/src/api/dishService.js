import useApi from "../hooks/useApi";

export const getDishes = async (token) => {
  const api = useApi(token);
  const response = await api.get("/api/Dish");
  return response.data.data;
};

export const getExtendedDish = async (token, id) => {
  const api = useApi(token);
  const response = await api.get("/api/Dish/" + id);
  return response.data.data;
};

export const createDish = async (token, body) => {
  const api = useApi(token);
  const response = await api.post("/api/Dish", body, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
  return response.data.data;
};

export const updateDish = async (token, body) => {
  const api = useApi(token);
  const response = await api.put("/api/Dish", body, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
  return response.data.data;
};

export const updateListOfDishProducts = async (token, body) => {
  const api = useApi(token);
  const response = await api.put("/update/listOfProducts", body);
  return response.data.data;
};

export const deleteDish = async (token, id) => {
  const api = useApi(token);
  const response = await api.delete("/api/Dish/" + id);
  return response.data.data;
};
