import { createContext, useState } from "react";
import { getExercises } from "../api/exerciseService";
import { getProducts } from "../api/productService";
import { getDishes } from "../api/dishService";

const StorageContext = createContext();

export const StorageProvider = ({ children }) => {
  const [exercises, setExercises] = useState([]);
  const [products, setProducts] = useState([]);
  const [dishes, setDishes] = useState([]);

  const fetchExercises = async (token) => {
    try {
      const data = await getExercises(token);
      setExercises(data);
    } catch (err) {
      console.log(err);
    }
  };
  const fetchProducts = async (token) => {
    try {
      setProducts(await getProducts(token));
    } catch (err) {
      console.log(err);
    }
  };
  const fetchDishes = async (token) => {
    try {
      setDishes(await getDishes(token));
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <StorageContext.Provider
      value={{
        exercises,
        products,
        dishes,
        fetchExercises,
        fetchProducts,
        fetchDishes,
      }}
    >
      {children}
    </StorageContext.Provider>
  );
};

export default StorageContext;
