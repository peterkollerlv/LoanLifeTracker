-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 198.71.227.89:3306
-- Generation Time: Apr 07, 2016 at 03:30 AM
-- Server version: 5.5.43-37.2-log
-- PHP Version: 5.5.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `cyberHostBox`
--

-- --------------------------------------------------------

--
-- Table structure for table `LoanLifeTrackerPayments`
--

CREATE TABLE `LoanLifeTrackerPayments` (
  `loanGuid` varchar(100) NOT NULL,
  `paymentDate` datetime DEFAULT NULL,
  `paymentTotalAmount` decimal(10,0) DEFAULT NULL,
  `paymentInterestAmount` decimal(10,0) DEFAULT NULL,
  `paymentPrincipalAmount` decimal(10,0) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `LoanLifeTrackerPayments`
--
ALTER TABLE `LoanLifeTrackerPayments`
  ADD PRIMARY KEY (`loanGuid`) USING BTREE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
