-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 198.71.227.89:3306
-- Generation Time: Apr 07, 2016 at 03:29 AM
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
-- Table structure for table `LoanLifeTracker`
--

CREATE TABLE `LoanLifeTracker` (
  `loanGuid` varchar(100) NOT NULL DEFAULT '',
  `loanTitle` varchar(100) DEFAULT NULL,
  `loanBeneficiary` varchar(100) DEFAULT NULL,
  `loanCollectionAccount` varchar(100) DEFAULT NULL,
  `loanCompanyInfo` varchar(100) DEFAULT NULL,
  `loanCurrency` varchar(10) DEFAULT NULL,
  `loanHasPenalty` tinyint(1) NOT NULL,
  `loanInitialLoanAmount` decimal(10,0) DEFAULT NULL,
  `loanPenaltyDate` date DEFAULT NULL,
  `loanPenaltyRate` decimal(10,0) DEFAULT NULL,
  `loanInterestRate` decimal(10,0) DEFAULT NULL,
  `loanInterestStructure` int(11) DEFAULT NULL,
  `loanLender` varchar(100) DEFAULT NULL,
  `loanPaid` tinyint(1) NOT NULL,
  `loanSavedToDB` tinyint(1) NOT NULL,
  `loanStartDate` date DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `LoanLifeTracker`
--

INSERT INTO `LoanLifeTracker` (`loanGuid`, `loanTitle`, `loanBeneficiary`, `loanCollectionAccount`, `loanCompanyInfo`, `loanCurrency`, `loanHasPenalty`, `loanInitialLoanAmount`, `loanPenaltyDate`, `loanPenaltyRate`, `loanInterestRate`, `loanInterestStructure`, `loanLender`, `loanPaid`, `loanSavedToDB`, `loanStartDate`) VALUES
('1501b1df-b1f0-417f-8377-3ec5617ae3d4', 'This is a long title', 'beneficiary', 'collection accoutn', 'Freeway CAM B.V.', 'USD', 0, '0', '0001-01-01', '10', '5', 0, 'Lender', 0, 1, '2016-04-07'),
('99137479-fbf3-4373-b846-c74925352e14', 'New This Long title', 'Beneficiary ', 'Coll account', 'Freeway CAM B.V.', 'USD', 0, '5000', '0001-01-01', '10', '5', 0, 'Lender info', 0, 1, '2016-04-07'),
('281ccc54-62a2-4086-bfc0-d11acdb51879', 'New Title', 'beneficiary', 'Collection account', 'Freeway CAM B.V.', 'USD', 0, '50000', '0001-01-01', '10', '5', 0, 'lender info', 0, 1, '2016-04-07');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `LoanLifeTracker`
--
ALTER TABLE `LoanLifeTracker`
  ADD PRIMARY KEY (`loanGuid`) USING BTREE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
