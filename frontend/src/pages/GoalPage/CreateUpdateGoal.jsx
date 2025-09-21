import { Button, IconButton, Stack, TextField } from "@mui/material";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import useInput from "../../hooks/useInput";
import { useContext, useEffect, useState } from "react";
import UpdateButton from "../../components/UpdateButton/UpdateButton";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";
import { createGoal, updateGoal } from "../../api/goalService";
import AuthContext from "../../store/AuthContext";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import dayjs from "dayjs";
import { getFormatedDate } from "../../common/helpers";
import {
  createWorkout,
  deleteWorkout,
  getWorkouts,
  updateListOfExercises,
  updateWorkout,
} from "../../api/workoutService";
import WorkoutsFragment from "./Workouts/WorkoutsFragment";
import WorkoutsContext, {
  WorkoutsProvider,
} from "../../store/Workouts/WorkoutsContext";
import DietsContext from "../../store/Diets/DietsContext";
import {
  createDiet,
  deleteDiet,
  getDiets,
  updateDiet,
  updateListOfProductsAndDishes,
} from "../../api/dietService";
import DietFragment from "./Diets/DietFragment";

const errMsg = "Ошибка ввода";

const defaultGoal = {
  name: "Моя новая цель",
  description: "Цель - достичь моей новой цели",
  deadline: "",
  diets: [],
  workouts: [],
};

const CreateUpdateGoal = ({ goal, onClickBack }) => {
  const { token, email } = useContext(AuthContext);
  const wctx = useContext(WorkoutsContext);
  const dctx = useContext(DietsContext);

  const isGoalCreating = goal === undefined || goal === null;

  const [currentGoal, setCurrentGoal] = useState(goal ?? defaultGoal);
  const [existedWorkouts, setExistedWorkputs] = useState([]);
  const [existedDiets, setExistedDiets] = useState([]);

  const name = useInput(currentGoal.name, (str) =>
    str !== "" && str !== " " && str !== null
      ? { isValid: true, err: null }
      : { isValid: false, err: errMsg }
  );
  const description = useInput(currentGoal.description, (str) => ({
    isValid: true,
    err: null,
  }));
  const [deadline, setDeadline] = useState(
    isGoalCreating ? dayjs() : dayjs(goal.deadline)
  );

  const getWorkoutsAndDietsByGoalId = async (id) => {
    try {
      if (isGoalCreating) {
        wctx.init([]);
        dctx.init([]);
      } else {
        const workouts = await getWorkouts(token, id);
        setExistedWorkputs(workouts);
        wctx.init(workouts);

        const diets = await getDiets(token, id);
        setExistedDiets(diets);
        dctx.init(diets);
      }
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    getWorkoutsAndDietsByGoalId(goal?.id);
  }, []);

  const createWorkoutAsync = async (name, goalId) => {
    return await createWorkout(token, {
      name: name,
      goalId: goalId,
    });
  };

  const updateExercises = async (we, wId) => {
    const exercises = we.reduce((acc, obj) => {
      acc[obj.id] = obj.repetitions;
      return acc;
    }, {});
    await updateListOfExercises(token, {
      id: wId,
      exercisesWithRepetitions: exercises,
    });
  };

  const createDietAsync = async (name, goalId) => {
    return await createDiet(token, {
      name: name,
      goalId: goalId,
    });
  };

  const updateListOfFood = async (produsctList, dishList, dietId) => {
    const products = produsctList.reduce((acc, obj) => {
      acc[obj.id] = obj.weight;
      return acc;
    }, {});
    const diets = dishList.reduce((acc, obj) => {
      acc[obj.id] = obj.weight;
      return acc;
    }, {});
    await updateListOfProductsAndDishes(token, {
      id: dietId,
      productsWithWeight: products,
      dishesWithWeight: diets,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    let body = {
      name: name.value,
      description: description.value,
      userEmail: email,
      deadline: getFormatedDate(deadline),
    };

    try {
      if (isGoalCreating) {
        const newGoalId = await createGoal(token, body);

        // создаем все добавленные тренировки и сохраняем для них все упражнения
        for (let i = 0; i < wctx.workouts.length; i++) {
          const workout = wctx.workouts[i];
          const newWorkoutId = await createWorkout(token, {
            name: workout.name,
            goalId: newGoalId,
          });
          // TODO вызвать уже написанный метод для этого
          const exercises = workout.exercises?.reduce((acc, obj) => {
            acc[obj.id] = obj.repetitions;
            return acc;
          }, {});
          await updateListOfExercises(token, {
            id: newWorkoutId,
            exercisesWithRepetitions: exercises,
          });
        }

        // создаем все добавленные диеты и сохраняем для них все блюда и продукты
        for (let i = 0; i < dctx.diets.length; i++) {
          const diet = dctx.diets[i];
          const newDietId = await createDiet(token, {
            name: diet.name,
            goalId: newGoalId,
          });
          await updateListOfFood(diet.products, diet.dishes, newDietId);
        }
      } else {
        body = {
          id: goal.id,
          name: body.name,
          description: body.description,
          deadline: body.deadline,
        };
        await updateGoal(token, body);

        // удаляем сначала те тренировки, что были в базе, но были удалены пользователем
        const listToDeleteWorkouts = existedWorkouts.filter(
          (w) => !wctx.workouts.map((ww) => ww.id).includes(w.id)
        );
        for (let i = 0; i < existedWorkouts.length; i++) {
          if (
            listToDeleteWorkouts.find(
              (item) => item.id === existedWorkouts[i].id
            ) !== undefined
          ) {
            await deleteWorkout(token, existedWorkouts[i].id);
          }
        }

        for (let i = 0; i < wctx.workouts.length; i++) {
          const workout = wctx.workouts[i];
          let workoutId = workout.id;

          if (
            existedWorkouts.find((ex) => ex.id === workoutId) !== undefined &&
            workout.exercises.length !== 0
          ) {
            await updateWorkout(token, {
              id: workoutId,
              name: workout.name,
            });
            await updateExercises(workout.exercises, workoutId);
          } else if (workout.exercises.length !== 0) {
            workoutId = await createWorkoutAsync(workout.name, goal.id);
            await updateExercises(workout.exercises, workoutId);
          } else {
            try {
              await deleteWorkout(token, workoutId);
            } catch {
              console.log("еще не создан");
            }
          }
        }

        // удаляем сначала те диеты, что были в базе, но были удалены пользователем
        const listToDeleteDiets = existedDiets.filter(
          (d) => !dctx.diets.map((dd) => dd.id).includes(d.id)
        );
        for (let i = 0; i < existedDiets.length; i++) {
          if (
            listToDeleteDiets.find((item) => item.id === existedDiets[i].id) !==
            undefined
          ) {
            await deleteDiet(token, existedDiets[i].id);
          }
        }

        for (let i = 0; i < dctx.diets.length; i++) {
          const diet = dctx.diets[i];
          let dietId = diet.id;

          if (existedDiets.find((ex) => ex.id === dietId) !== undefined) {
            await updateDiet(token, {
              id: dietId,
              name: diet.name,
            });
          } else {
            dietId = await createDietAsync(diet.name, goal.id);
          }

          if (
            (diet.products !== undefined && diet.products.length !== 0) ||
            (diet.dishes !== undefined && diet.dishes.length !== 0)
          ) {
            await updateListOfFood(
              diet.products ?? [],
              diet.dishes ?? [],
              dietId
            );
          } else {
            try {
              await deleteDiet(token, dietId);
            } catch {
              console.log("еще не создан");
            }
          }
        }
      }
      // сбрасываем контексты
      wctx.init([]);
      dctx.init([]);
      onClickBack();
    } catch (err) {
      console.log(err);
    }
  };

  const handleClickBack = (e) => {
    onClickBack();
    wctx.init([]);
    dctx.init([]);
  };

  return (
    <>
      <form onSubmit={handleSubmit}>
        <Stack direction="row" sx={{ marginBottom: 4 }}>
          <Button
            variant="contained"
            startIcon={<ArrowBackIcon />}
            onClick={handleClickBack}
          >
            Назад
          </Button>
          <UpdateButton
            sx={{ marginLeft: "auto", marginRight: 4 }}
            type="submit"
          >
            <EditOutlinedIcon />
          </UpdateButton>
        </Stack>
        <Stack direction="row" sx={{ gap: 2 }}>
          <TextField
            label="Название"
            value={name.value}
            onChange={name.onchange}
            error={name.error !== null}
            helperText={name.error}
          />
          <DatePicker
            value={deadline}
            onChange={(val) => setDeadline(val)}
            label="Дедлайн"
          />
        </Stack>
        <TextField
          label="Описание"
          multiline
          rows={8}
          value={description.value}
          onChange={description.onchange}
          error={description.error !== null}
          helperText={description.error}
          sx={{ minWidth: 500, marginTop: 3 }}
        />
      </form>
      <Stack sx={{ marginTop: 4 }}>
        <DietFragment sx={{}} />
        <WorkoutsFragment sx={{ marginTop: 8 }} />
      </Stack>
    </>
  );
};

export default CreateUpdateGoal;
