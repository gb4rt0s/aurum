"use client"

import { useEffect, useState } from "react";
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from "recharts";
import { fetchExpensesByDate } from "@/scripts/dashboard_scripts/dashboard_scripts";
import ChangeChartAcc from "./chart-utils/change-chart-acc";
import ChangeDaysShown from "./chart-utils/change-days-shown";
import ChangeChartForm from '../change-chart-form';

export default function ExpenseLineChart({ isEditMode, accounts, segmentIndex, chosenLayout, chosenCharts, possibleChartsBySegment, setChosenCharts, chartLoaded }) {

    const chartName = "expense-line-chart"

    const [expensesByDateString, setExpensesByDateString] = useState([])
    const [startDate, setStartDate] = useState(new Date())
    const [today, setToday] = useState(new Date())
    const [chartData, setChartData] = useState([])
    const [rawChartData, setRawChartData] = useState([])
    const [curAccount, setCurAccount] = useState()
    const [daysShown, setDaysShown] = useState(10)

    useEffect(() => {
        let nDaysAgo = new Date()
        nDaysAgo.setDate(today.getDate() - daysShown);
        setStartDate(nDaysAgo)
    }, [daysShown])

    useEffect(() => {
        if (accounts) {
            setCurAccount(accounts[0])
        }
    }, [accounts])

    useEffect(() => {
        const getBalances = async (accId) => {
            const updatedExpenses = await fetchExpensesByDate(accId, startDate.toISOString().slice(0, 10), today.toISOString().slice(0, 10))
            let expensesByDate = Object.groupBy(updatedExpenses, ({ date }) => date)
            let updatedExpenseByDateString = []
            for (const key in expensesByDate) {
                updatedExpenseByDateString[key.toString().slice(0, 10)] = expensesByDate[key]
            }
            setExpensesByDateString(updatedExpenseByDateString)
            chartLoaded(chartName)
        }
        if (curAccount) {
            getBalances(curAccount.accountId)
        }
    }, [curAccount, startDate])



    useEffect(() => {
        let updatedChartData = []
        for (let i = 0; i <= daysShown; i++) {
            let curDate = new Date(startDate)
            curDate.setDate(startDate.getDate() + i)
            let dateString = curDate.toISOString().slice(0, 10)

            if (expensesByDateString[dateString]) {
                let sum = 0
                for (const expense of expensesByDateString[dateString]) {
                    sum += expense.amount
                }
                updatedChartData[dateString] = {
                    name: dateString,
                    expense: sum
                }
            } else {
                updatedChartData[dateString] = {
                    name: dateString,
                    expense: 0
                }
            }
        }
        setChartData(updatedChartData)
    }, [startDate, expensesByDateString])


    useEffect(() => {
        let updatedRowChartData = []
        for (const key in chartData) {
            updatedRowChartData.push(chartData[key])
        }
        setRawChartData(updatedRowChartData)
    }, [chartData])

    const handleChangeType = (e) => {
        const accName = e.target.value
        const updatedCurAcc = accounts.find(a => a.displayName == accName)
        setCurAccount(updatedCurAcc)
    }

    const handleChangeDays = (e) => {
        setDaysShown(e.target.value)
    }

    return (
        <div key={segmentIndex} className={`${chosenLayout}-${segmentIndex + 1} chart-container ${isEditMode && "edit-mode"}`}>

            <div className='chart-title-container'>
                {isEditMode &&
                    <div className="change-chart-types-container">
                        <ChangeChartAcc handleChangeType={handleChangeType} accounts={accounts} curAccount={curAccount} />
                        <ChangeDaysShown handleChangeDays={handleChangeDays} daysShown={daysShown} />
                        <ChangeChartForm
                            chosenCharts={chosenCharts}
                            segmentIndex={segmentIndex}
                            possibleCharts={possibleChartsBySegment[segmentIndex]}
                            setChosenCharts={setChosenCharts} />
                    </div>
                }
                <div className="chart-title">
                    <p>Expenses in last <span className='highlight'>{daysShown}</span> days #<span className='highlight'>{curAccount && curAccount.displayName}</span></p>
                </div>
            </div>

            <div className="chart">
                <ResponsiveContainer width="100%" height={200} className="chart-body">
                    <LineChart data={rawChartData}>
                        <CartesianGrid strokeDasharray="3 3" />
                        <XAxis dataKey="name" />
                        <YAxis />
                        <Tooltip
                            contentStyle={{ backgroundColor: "#333333", borderColor: "#F9D342", color: "#F4F4F4" }}
                        />
                        <Legend />
                        <Line type="monotone" dataKey="expense" stroke="#F9D342" name={curAccount && `${curAccount.displayName} (${curAccount.currency.currencyCode})`} />
                    </LineChart>
                </ResponsiveContainer>
            </div>
        </div>
    )
}