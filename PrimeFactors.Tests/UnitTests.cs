using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeFactors.Interfaces;
using Moq;
using PrimeFactors.Algorithms;
using System;
using PrimeFactors.Models;
using PrimeFactors.Resources;
using PrimeFactors.ViewModels;
using System.Collections.ObjectModel;

namespace PrimeFactors.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestMainWindowModel_PrimeFactors_Pass()
        {
            var mockModel = new Mock<IMainWindowModel>();

            mockModel.SetupProperty(model => model.Algorithms, new ObservableCollection<AlgorithmItemModel>() { new AlgorithmItemModel(new PrimeFactorsAlgorithm()) });

            mockModel.SetupProperty(model => model.SelectedAlgorithm, mockModel.Object.Algorithms[0]);

            mockModel.SetupProperty(model => model.Input, "123");

            var viewModel = new MainWindowViewModel(mockModel.Object);

            Utils.CalculationResult expectedResult = new Utils.CalculationResult(true, "Prime factor(s): 3, 41");

            Utils.CalculationResult actualResult = viewModel.RunCalculation();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestMainWindowModel_PrimeFactors_GreaterThanOne()
        {
            var mockModel = new Mock<IMainWindowModel>();

            mockModel.SetupProperty(model => model.Algorithms, new ObservableCollection<AlgorithmItemModel>() { new AlgorithmItemModel(new PrimeFactorsAlgorithm()) });

            mockModel.SetupProperty(model => model.SelectedAlgorithm, mockModel.Object.Algorithms[0]);

            mockModel.SetupProperty(model => model.Input, "-123");

            var viewModel = new MainWindowViewModel(mockModel.Object);

            Utils.CalculationResult expectedResult = new Utils.CalculationResult(false, Utils.Message_GreaterThanOne);

            Utils.CalculationResult actualResult = viewModel.RunCalculation();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestMainWindowModel_PrimeFactors_NonNegativeInteger()
        {
            var mockModel = new Mock<IMainWindowModel>();

            mockModel.SetupProperty(model => model.Algorithms, new ObservableCollection<AlgorithmItemModel>() { new AlgorithmItemModel(new PrimeFactorsAlgorithm()) });

            mockModel.SetupProperty(model => model.SelectedAlgorithm, mockModel.Object.Algorithms[0]);

            mockModel.SetupProperty(model => model.Input, "Incorrect Input Format");

            var viewModel = new MainWindowViewModel(mockModel.Object);

            Utils.CalculationResult expectedResult = new Utils.CalculationResult(false, Utils.Message_NonNegativeInteger);

            Utils.CalculationResult actualResult = viewModel.RunCalculation();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestMainWindowModel_AddsFifty_Pass()
        {
            var mockModel = new Mock<IMainWindowModel>();

            mockModel.SetupProperty(model => model.Algorithms, new ObservableCollection<AlgorithmItemModel>() { new AlgorithmItemModel(new AddFiftyAlgorithm()) });

            mockModel.SetupProperty(model => model.SelectedAlgorithm, mockModel.Object.Algorithms[0]);

            mockModel.SetupProperty(model => model.Input, "123");

            var viewModel = new MainWindowViewModel(mockModel.Object);

            Utils.CalculationResult expectedResult = new Utils.CalculationResult(true, "173");

            Utils.CalculationResult actualResult = viewModel.RunCalculation();

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
