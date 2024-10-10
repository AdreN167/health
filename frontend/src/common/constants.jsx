import DishPage from "../pages/DishPage/DishPage";
import ExercisePage from "../pages/ExercisePage/ExercisePage";
import ProductPage from "../pages/ProductPage/ProductPage";
import TrainerPage from "../pages/TrainerPage/TrainerPage";

export const routes = [
  {
    path: "/product",
    label: "Продукты",
    element: () => <ProductPage />,
  },
  {
    path: "/dish",
    label: "Блюда",
    element: () => <DishPage />,
  },
  {
    path: "/trainer",
    label: "Тренажеры",
    element: () => <TrainerPage />,
  },
  {
    path: "/exercise",
    label: "Упражнения",
    element: () => <ExercisePage />,
  },
];
