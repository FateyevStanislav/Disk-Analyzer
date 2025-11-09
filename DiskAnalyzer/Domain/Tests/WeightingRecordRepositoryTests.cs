using NUnit.Framework;
using DiskAnalyzer.Domain;
using System;

namespace DiskAnalyzer.Domain.Tests
{
    [TestFixture]
    public class WeightingRecordRepositoryTests
    {
        private WeightingRecord _testRecord;
        private Guid _testId;

        [SetUp]
        public void Setup()
        {
            _testId = Guid.NewGuid();
            _testRecord = new WeightingRecord(
                _testId,
                @"C:\Users\Ярослав\Desktop\схрон",
                2,
                3,
                null);
        }

        [TearDown]
        public void TearDown()
        {
            WeightingRecordRepository.Clear();
        }

        [Test]
        public void AddRecord_WithValidRecord_ShouldAddSuccessfully()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => WeightingRecordRepository.AddRecord(_testRecord));
        }

        [Test]
        public void AddRecord_WithDuplicateId_ShouldThrowArgumentException()
        {
            // Arrange
            WeightingRecordRepository.AddRecord(_testRecord);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                WeightingRecordRepository.AddRecord(_testRecord));

            Assert.That(ex.Message, Contains.Substring("Exception in AddRecord Method"));
        }

        [Test]
        public void AddRecord_WithNullRecord_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                WeightingRecordRepository.AddRecord(null));
        }

        [Test]
        public void GetRecord_WithExistingId_ShouldReturnRecord()
        {
            // Arrange
            WeightingRecordRepository.AddRecord(_testRecord);

            // Act
            var result = WeightingRecordRepository.GetRecord(_testId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(_testId));
                Assert.That(result.Path, Is.EqualTo(_testRecord.Path));
                Assert.That(result.MaxDepth, Is.EqualTo(_testRecord.MaxDepth));
                Assert.That(result.FileCount, Is.EqualTo(_testRecord.FileCount));
            });
        }

        [Test]
        public void GetRecord_WithNonExistingId_ShouldThrowArgumentException()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                WeightingRecordRepository.GetRecord(nonExistingId));

            Assert.That(ex.Message, Contains.Substring("Exception in GetRecord Method"));
        }

        [Test]
        public void RemoveRecord_WithExistingId_ShouldRemoveSuccessfully()
        {
            // Arrange
            WeightingRecordRepository.AddRecord(_testRecord);

            // Act & Assert
            Assert.DoesNotThrow(() => WeightingRecordRepository.RemoveRecord(_testId));
        }

        [Test]
        public void RemoveRecord_WithNonExistingId_ShouldThrowArgumentException()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() =>
                WeightingRecordRepository.RemoveRecord(nonExistingId));

            Assert.That(ex.Message, Contains.Substring("Exception in RemoveRecord Method"));
        }

        [Test]
        public void AddGetRemove_IntegrationTest_ShouldWorkCorrectly()
        {
            // Arrange
            var record1 = new WeightingRecord(Guid.NewGuid(), "Path1", 1, 10, null);
            var record2 = new WeightingRecord(Guid.NewGuid(), "Path2", 2, 20, "Error");

            // Act & Assert - Add
            Assert.DoesNotThrow(() => WeightingRecordRepository.AddRecord(record1));
            Assert.DoesNotThrow(() => WeightingRecordRepository.AddRecord(record2));

            // Act & Assert - Get
            var retrieved1 = WeightingRecordRepository.GetRecord(record1.Id);
            var retrieved2 = WeightingRecordRepository.GetRecord(record2.Id);

            Assert.That(retrieved1.Path, Is.EqualTo("Path1"));
            Assert.That(retrieved2.Error, Is.EqualTo("Error"));

            // Act & Assert - Remove
            Assert.DoesNotThrow(() => WeightingRecordRepository.RemoveRecord(record1.Id));
            Assert.Throws<ArgumentException>(() => WeightingRecordRepository.GetRecord(record1.Id));
            Assert.DoesNotThrow(() => WeightingRecordRepository.GetRecord(record2.Id));
        }

        [Test]
        public void MultipleRecords_WithDifferentIds_ShouldCoexist()
        {
            // Arrange
            var records = new[]
            {
                new WeightingRecord(Guid.NewGuid(), "Path1", 1, 10, null),
                new WeightingRecord(Guid.NewGuid(), "Path2", 2, 20, null),
                new WeightingRecord(Guid.NewGuid(), "Path3", 3, 30, null)
            };

            // Act
            foreach (var record in records)
            {
                WeightingRecordRepository.AddRecord(record);
            }

            // Assert
            foreach (var record in records)
            {
                var retrieved = WeightingRecordRepository.GetRecord(record.Id);
                Assert.That(retrieved, Is.Not.Null);
            }
        }
    }
}