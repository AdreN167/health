import { useContext, useEffect, useMemo, useState } from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Select,
  MenuItem,
  InputLabel,
  FormControl,
  Button,
  Stack,
} from "@mui/material";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import dayjs from "dayjs";
import { getDietEvents } from "../../api/dietEventJournalService";
import AuthContext from "../../store/AuthContext";
import { getFormatedDate } from "../../common/helpers";
import { getGoalsByUserEmail } from "../../api/goalService";
import { getDiets } from "../../api/dietService";
import PdfContext from "../../store/PdfContext";

const createDate = (date) =>
  date
    ? dayjs(date)
        .set("second", 0)
        .set("hour", 0)
        .set("minute", 0)
        .set("millisecond", 0)
    : dayjs()
        .set("second", 0)
        .set("hour", 0)
        .set("minute", 0)
        .set("millisecond", 0);

const DietJournal = ({ sx, mealsDate, diets, getResult }) => {
  const { token, email } = useContext(AuthContext);
  const ctx = useContext(PdfContext);

  const [startDate, setStartDate] = useState(createDate().add(-1, "month"));
  const [endDate, setEndDate] = useState(createDate());
  const [selectedDiet, setSelectedDiet] = useState("");

  const filteredMeals = mealsDate.filter((meal) => {
    const mealDate = new Date(meal.date);
    const isWithinDateRange =
      (!startDate || mealDate >= new Date(startDate)) &&
      (!endDate || mealDate <= new Date(endDate));
    const isDietMatch = !selectedDiet || meal.dietId === selectedDiet;
    return isWithinDateRange && isDietMatch;
  });

  const [sortConfig, setSortConfig] = useState({
    key: "date",
    direction: "descending",
  });
  const sortedData = useMemo(() => {
    let sortableItems = [...filteredMeals];
    if (sortConfig !== null) {
      sortableItems.sort((a, b) => {
        if (a[sortConfig.key] < b[sortConfig.key]) {
          return sortConfig.direction === "ascending" ? -1 : 1;
        }
        if (a[sortConfig.key] > b[sortConfig.key]) {
          return sortConfig.direction === "ascending" ? 1 : -1;
        }
        return 0;
      });
    }
    return sortableItems;
  }, [filteredMeals, sortConfig]);

  useEffect(() => {
    getResult(
      sortedData.map((i) => ({
        ...i,
        diet: diets.find((w) => w.id === i.dietId)?.name,
      }))
    );
  }, [filteredMeals, sortConfig]);

  const requestSort = (key) => {
    let direction = "ascending";
    if (
      sortConfig &&
      sortConfig.key === key &&
      sortConfig.direction === "ascending"
    ) {
      direction = "descending";
    }
    setSortConfig({ key, direction });
  };

  return (
    <Stack sx={{ ...sx }}>
      <Stack direction="row">
        <FormControl variant="outlined" style={{ width: 150 }}>
          <InputLabel>Диета</InputLabel>
          <Select
            value={selectedDiet}
            onChange={(e) => setSelectedDiet(e.target.value)}
            label="Диета"
          >
            <MenuItem value="">
              <em>Все</em>
            </MenuItem>
            {diets.map((diet) => (
              <MenuItem key={diet.id} value={diet.id}>
                {diet.name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <DatePicker
          sx={{ maxWidth: 150, marginLeft: 2 }}
          value={startDate}
          onChange={(e) => setStartDate(e)}
          label="Дата начала"
        />
        <DatePicker
          sx={{ maxWidth: 150, marginLeft: 2 }}
          value={endDate}
          onChange={(e) => setEndDate(e)}
          label="Дата конца"
        />

        <Button
          variant="contained"
          color="primary"
          onClick={() => {
            setStartDate(createDate().add(-1, "month"));
            setEndDate(createDate());
            setSelectedDiet("");
          }}
          sx={{
            marginLeft: 2,
          }}
        >
          Сбросить фильтры
        </Button>
      </Stack>

      <TableContainer component={Paper} style={{ marginTop: "20px" }}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell
                sx={{
                  cursor: "pointer",
                  transition: "0.2s",
                  "&:hover": { bgcolor: "rgba(0, 0, 0, 0.1)" },
                }}
                onClick={() => requestSort("id")}
              >
                Id
              </TableCell>
              <TableCell
                sx={{
                  cursor: "pointer",
                  transition: "0.2s",
                  "&:hover": { bgcolor: "rgba(0, 0, 0, 0.1)" },
                }}
                onClick={() => requestSort("date")}
              >
                Дата
              </TableCell>
              <TableCell
                sx={{
                  cursor: "pointer",
                  transition: "0.2s",
                  "&:hover": { bgcolor: "rgba(0, 0, 0, 0.1)" },
                }}
                onClick={() => requestSort("calories")}
              >
                Калории
              </TableCell>
              <TableCell
                sx={{
                  cursor: "pointer",
                  transition: "0.2s",
                  "&:hover": { bgcolor: "rgba(0, 0, 0, 0.1)" },
                }}
                onClick={() => requestSort("proteins")}
              >
                Белки
              </TableCell>
              <TableCell
                sx={{
                  cursor: "pointer",
                  transition: "0.2s",
                  "&:hover": { bgcolor: "rgba(0, 0, 0, 0.1)" },
                }}
                onClick={() => requestSort("fats")}
              >
                Жиры
              </TableCell>
              <TableCell
                sx={{
                  cursor: "pointer",
                  transition: "0.2s",
                  "&:hover": { bgcolor: "rgba(0, 0, 0, 0.1)" },
                }}
                onClick={() => requestSort("carbohydrates")}
              >
                Углеводы
              </TableCell>
              <TableCell>Диета</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {sortedData.map((meal) => {
              return (
                <TableRow key={meal.id}>
                  <TableCell>{meal.id}</TableCell>
                  <TableCell>
                    {getFormatedDate(createDate(meal.date))}
                  </TableCell>
                  <TableCell>{meal.calories.toFixed(2)}</TableCell>
                  <TableCell>{meal.proteins.toFixed(2)}</TableCell>
                  <TableCell>{meal.fats.toFixed(2)}</TableCell>
                  <TableCell>{meal.carbohydrates.toFixed(2)}</TableCell>
                  <TableCell>
                    {diets.find((ad) => meal.dietId === ad.id)?.name}
                  </TableCell>
                </TableRow>
              );
            })}
          </TableBody>
        </Table>
      </TableContainer>
    </Stack>
  );
};

export default DietJournal;
